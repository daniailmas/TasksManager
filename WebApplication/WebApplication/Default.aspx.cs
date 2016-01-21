using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Tasks.v1;
using Google.Apis.Tasks.v1.Data;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;

namespace WebApplication
{
    public partial class Default : System.Web.UI.Page
    {
        private string title = "Default";
        private TasksService service = new TasksService();
        private TaskList list = new TaskList();
        protected void Page_Load(object sender, EventArgs e)
        {
                //User Credentional Authorization
                UserCredential credential;
                using (var stream = new FileStream(Server.MapPath("/client_secret.json"), FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        new[] { TasksService.Scope.Tasks },
                        "user", CancellationToken.None, new FileDataStore("Tasks.Auth.Store")).Result;
                }

                // Create the service.
                service = new TasksService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Tasks App",
                });
          
            //Check if default task list exist.
            // If yes show else create and show

            if (!ListExists(service, title))
            {
                //Create
                CreateTasklist(service);
                //Show
                ShowList(service);

            }
            else
            {
                //Show
                ShowList(service);
            }
            list = service.Tasklists.List().Execute().Items.Where(l => l.Title == "Default").SingleOrDefault();
        }
  
        private bool ListExists(TasksService service, string title)
        {
            return (from TaskList taskList in service.Tasklists.List().Execute().Items
                    where taskList.Title == title
                    select taskList).Count() > 0;
        }

        private void ShowList(TasksService service)
        {
            bltasklist.Items.Clear();
            TaskList list = service.Tasklists.List().Execute().Items.Where(l => l.Title == "Default").SingleOrDefault();
            var tasks = service.Tasks.List(list.Id).Execute();
            if(tasks.Items!= null)
            {
                foreach (var item in tasks.Items)
                {
                    if (item.Title != null)
                    {
                        bltasklist.Items.Add(item.Title);
                    }

                }
            }
            else
            {
                bltasklist.Items.Add("List Is Empty!");
            }
            
            
        }

        private void CreateTasklist(TasksService service)
        {
            var list = new TaskList();
            list.Title = title;
            list = service.Tasklists.Insert(list).Execute();
        }

        protected void btninsert_Click(object sender, EventArgs e)
        {
            if(txttitle.Text!="")
            {
                string t = txttitle.Text;
                Task task = new Task();
                task.Title = t;
                service.Tasks.Insert(task, list.Id).Execute();
                ShowList(service);
            }
        }

        protected void btndeleteall_Click(object sender, EventArgs e)
        {
            var tasks = service.Tasks.List(list.Id).Execute();
            foreach (var item in tasks.Items)
            {
                service.Tasks.Delete(list.Id, item.Id).Execute();
            }
            ShowList(service);
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            if(txtdeltitle.Text!="")
            {
                string t = txtdeltitle.Text;
                Task task = service.Tasks.List(list.Id).Execute().Items.Where(l => l.Title == t).SingleOrDefault();
                if (task != null)
                {
                    service.Tasks.Delete(list.Id, task.Id).Execute();
                    ShowList(service);

                }
                else
                {
                    Console.WriteLine("No task of this title");
                }
            }
        }
    }
}