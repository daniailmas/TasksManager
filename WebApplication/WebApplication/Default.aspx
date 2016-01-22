<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tasks</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="col-lg-11 col-lg-offset-1">
                <h1>Task List Manager</h1>
                <h3>Click on delete button to delete task.
                    Click on insert button to insert new task. 
                    Click delete all button to remove all tasks in list.
                </h3>
                <div class="container-fluid">
                    <h4>Default Task List:</h4>
                    <asp:BulletedList ID="bltasklist" runat="server" CssClass="list-group">
                    </asp:BulletedList>
                    <button type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#insert">Insert Task</button>
                    <button type="button" class="btn btn-warning btn-lg" data-toggle="modal" data-target="#delete">Delete Task</button>
                    <asp:Button ID="btndeleteall" Text="Delete All Tasks" runat="server" OnClick="btndeleteall_Click" CssClass="btn btn-danger btn-lg" />
                </div>

            </div>
        </div>
    

    <!-- Insert Modal -->
    <div id="insert" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Insert Task</h4>
                </div>
                <div class="modal-body">
                    <p>Enter Task name below:</p>
                    <div class="col-lg-4">
                        <asp:TextBox ID="txttitle" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btninsert" Text="Insert" runat="server" OnClick="btninsert_Click" CssClass="btn btn-primary" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>

     <!-- Delete Modal -->
    <div id="delete" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Delete Task</h4>
                </div>
                <div class="modal-body">
                    <p>Enter Task name below:</p>
                    <div class="col-lg-4">
                        <asp:TextBox ID="txtdeltitle" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btndelete"  Text="Delete Task" runat="server" CssClass="btn btn-danger" OnClick="btndelete_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>

    </form>

    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>
