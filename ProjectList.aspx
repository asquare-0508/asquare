<%@ Page Title="Dashbord" AutoEventWireup="true" Language="C#" CodeBehind="ProjectList.aspx.cs" Inherits="WebApplication1.ProjectList" %>

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>A-Square</title>

    <!-- Custom fonts for this template-->
    <link href="vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <link
        href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet">

    <!-- Custom styles for this template-->
    <link href="css/sb-admin-2.min.css" rel="stylesheet">

</head>

<body id="page-top">

    <!-- Page Wrapper -->
    <div id="wrapper" runat="server">

        <!-- Sidebar -->
        <ul class="navbar-nav bg-gradient-primary sidebar sidebar-dark accordion" id="accordionSidebar">

            <!-- Sidebar - Brand -->
            <a class="sidebar-brand d-flex align-items-center justify-content-center" href="index.html">
                <div class="sidebar-brand-icon rotate-n-15">
                    <i class="fas fa-laugh-wink"></i>
                </div>
                <div class="sidebar-brand-text mx-3">A-Square<sup>2</sup></div>
            </a>

            <!-- Divider -->
            <hr class="sidebar-divider my-0">

            <!-- Nav Item - Pages Collapse Menu -->
            <li class="nav-item">
                <a class="nav-link collapsed" href="#" data-toggle="collapse" data-target="#collapseTwo"
                    aria-expanded="true" aria-controls="collapseTwo">
                    <i class="fas fa-fw fa-cog"></i>
                    <span>Personal Information</span>
                </a>
                <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#accordionSidebar">
                    <div class="bg-white py-2 collapse-inner rounded">
                        <a class="collapse-item" href="AddNewClient.aspx">Add / Update Personal Info</a>
                        <a class="collapse-item" href="Investors.aspx">Create Investors</a>
                        <a class="collapse-item" href="InvestorList.aspx">Investor Informations</a>
                    </div>
                </div>
            </li>

            <!-- Nav Item - Utilities Collapse Menu -->
            <li class="nav-item">
                <a class="nav-link collapsed" href="#" data-toggle="collapse" data-target="#Receipting"
                    aria-expanded="true" aria-controls="Receipting">
                    <i class="fas fa-fw fa-cog"></i>
                    <span>Accounts</span>
                </a>
                <div id="Receipting" class="collapse" aria-labelledby="headingUtilities"
                    data-parent="#accordionSidebar">
                    <div class="bg-white py-2 collapse-inner rounded">
                        <a class="collapse-item" href="AddMoney.aspx">Add Money</a>
                        <a class="collapse-item" href="ReceiptingMovement.aspx">View Receipting Data</a>
                        <a class="collapse-item" href="ReceiptingMovement.aspx">Investor Money Moment</a>
                    </div>
                </div>
            </li>

            <!-- Nav Item - Utilities Collapse Menu -->
            <li class="nav-item">
                <a class="nav-link collapsed" href="#" data-toggle="collapse" data-target="#Entity"
                    aria-expanded="true" aria-controls="Entity">
                    <i class="fas fa-fw fa-cog"></i>
                    <span>Entity Module</span>
                </a>
                <div id="Entity" class="collapse" aria-labelledby="headingUtilities"
                    data-parent="#accordionSidebar">
                    <div class="bg-white py-2 collapse-inner rounded">
                        <a class="collapse-item" href="AddNewCompany.aspx">New Company</a>
                        <a class="collapse-item" href="AddProjects.aspx">Create / Update  Project</a>
                        <a class="collapse-item" href="AmountAllocation.aspx">Create Amount Allocation</a>
                        <a class="collapse-item" href="ProjectList.aspx">Project List</a>
                    </div>
                </div>
            </li>
        </ul>
        <!-- End of Sidebar -->

        <!-- Content Wrapper -->
        <div id="content-wrapper" class="d-flex flex-column">

            <!-- Main Content -->
            <div id="content">

                <!-- Topbar -->
                <nav class="navbar navbar-expand navbar-light bg-white topbar mb-4 static-top shadow">

                    <!-- Sidebar Toggle (Topbar) -->
                    <button id="sidebarToggleTop" class="btn btn-link d-md-none rounded-circle mr-3">
                        <i class="fa fa-bars"></i>
                    </button>
                    <!-- Topbar Search -->

                    <div class="txt-center"> 
                         <asp:Label ID="lblrtnMsg" CssClass="h4 text-gray-900 mb-4" runat="server" ></asp:Label>
                    </div>


                    <!-- Topbar Navbar -->
                    <ul class="navbar-nav ml-auto">

                        <div class="topbar-divider d-none d-sm-block"></div>

                        <!-- Nav Item - User Information -->
                        <li class="nav-item dropdown no-arrow">
                            <a class="nav-link dropdown-toggle" href="#" id="userDropdown" role="button"
                                data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <span class="mr-2 d-none d-lg-inline text-gray-600 small">Douglas McGee</span>
                                <img class="img-profile rounded-circle"
                                    src="img/undraw_profile.svg">
                            </a>
                            <!-- Dropdown - User Information -->
                            <div class="dropdown-menu dropdown-menu-right shadow animated--grow-in"
                                aria-labelledby="userDropdown">
                                <a class="dropdown-item" href="#">
                                    <i class="fas fa-user fa-sm fa-fw mr-2 text-gray-400"></i>
                                    Profile
                                </a>
                                <a class="dropdown-item" href="#">
                                    <i class="fas fa-cogs fa-sm fa-fw mr-2 text-gray-400"></i>
                                    Settings
                                </a>
                                <a class="dropdown-item" href="#">
                                    <i class="fas fa-list fa-sm fa-fw mr-2 text-gray-400"></i>
                                    Activity Log
                                </a>
                                <div class="dropdown-divider"></div>
                                <a class="dropdown-item" href="#" data-toggle="modal" data-target="#logoutModal">
                                    <i class="fas fa-sign-out-alt fa-sm fa-fw mr-2 text-gray-400"></i>
                                    Logout
                                </a>
                            </div>
                        </li>
                    </ul>
                </nav>
                <!-- End of Topbar -->


<!--Page Content asraf -->
<form runat="server">
<div class="container-fluid">

<div class="row">

                        <!-- Earnings (Monthly) Card Example -->
                        <div class="col-xl-3 col-md-6 mb-4">
                            <div class="card border-left-primary shadow h-100 py-2">
                                <div class="card-body">
                                    <div class="row no-gutters align-items-center">
                                        <div class="col mr-2">
                                            <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">
                                                Total Approved</div>
                                            <asp:Label Id="lblApproved" runat="server" class="h5 mb-0 font-weight-bold text-gray-800"></asp:Label>
                                        </div>
                                        <div class="col-auto">
                                            <i class="fas fa-calendar fa-2x text-gray-300"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Earnings (Monthly) Card Example -->
                        <div class="col-xl-3 col-md-6 mb-4">
                            <div class="card border-left-success shadow h-100 py-2">
                                <div class="card-body">
                                    <div class="row no-gutters align-items-center">
                                        <div class="col mr-2">
                                            <div class="text-xs font-weight-bold text-success text-uppercase mb-1">
                                                Total Allocated</div>
                                                <asp:Label Id="lblAllocated" runat="server" class="h5 mb-0 font-weight-bold text-gray-800"></asp:Label>
                                        </div>
                                        <div class="col-auto">
                                            <i class="fas fa-dollar-sign fa-2x text-gray-300"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Earnings (Monthly) Card Example -->
                        <div class="col-xl-3 col-md-6 mb-4">
                            <div class="card border-left-primary shadow h-100 py-2">
                                <div class="card-body">
                                    <div class="row no-gutters align-items-center">
                                        <div class="col mr-2">
                                            <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">
                                                Total Admin Charge</div>
                                            <asp:Label Id="lblAdminCharge" runat="server" class="h5 mb-0 font-weight-bold text-gray-800"></asp:Label>
                                        </div>
                                        <div class="col-auto">
                                            <i class="fas fa-calendar fa-2x text-gray-300"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Earnings (Monthly) Card Example -->
                        <div class="col-xl-3 col-md-6 mb-4">
                            <div class="card border-left-success shadow h-100 py-2">
                                <div class="card-body">
                                    <div class="row no-gutters align-items-center">
                                        <div class="col mr-2">
                                            <div class="text-xs font-weight-bold text-success text-uppercase mb-1">
                                                Total Other Charges</div>
                                                <asp:Label Id="lblOtherCharge" runat="server" class="h5 mb-0 font-weight-bold text-gray-800"></asp:Label>
                                        </div>
                                        <div class="col-auto">
                                            <i class="fas fa-dollar-sign fa-2x text-gray-300"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-xl-3 col-md-6 mb-4">
                            <div class="card border-left-success shadow h-100 py-2">
                                <div class="card-body">
                                    <div class="row no-gutters align-items-center">
                                        <div class="col mr-2">
                                            <div class="text-xs font-weight-bold text-success text-uppercase mb-1">
                                                Total Profit</div>
                                                <asp:Label Id="lblProfit" runat="server" class="h5 mb-0 font-weight-bold text-gray-800"></asp:Label>
                                        </div>
                                        <div class="col-auto">
                                            <i class="fas fa-dollar-sign fa-2x text-gray-300"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-xl-3 col-md-6 mb-4">
                            <div class="card border-left-success shadow h-100 py-2">
                                <div class="card-body">
                                    <div class="row no-gutters align-items-center">
                                        <div class="col mr-2">
                                            <div class="text-xs font-weight-bold text-success text-uppercase mb-1">
                                                Total Profit Percentage</div>
                                                <asp:Label Id="totProfitPercent" runat="server" class="h5 mb-0 font-weight-bold text-gray-800"></asp:Label>
                                        </div>
                                        <div class="col-auto">
                                            <i class="fas fa-dollar-sign fa-2x text-gray-300"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-xl-3 col-md-6 mb-4">
                            <div class="card border-left-warning shadow h-100 py-2">
                                <div class="card-body">
                                    <div class="row no-gutters align-items-center">
                                        <div class="col mr-2">
                                            <div class="text-xs font-weight-bold text-success text-uppercase mb-1">
                                                Total Profit Percentage</div>
                                                <asp:Label Id="Label3" runat="server" class="h5 mb-0 font-weight-bold text-gray-800"></asp:Label>
                                        </div>
                                        <div class="col-auto">
                                            <i class="fas fa-dollar-sign fa-2x text-gray-300"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-xl-3 col-md-6 mb-4">
                            <div class="card border-left-fail shadow h-100 py-2">
                                <div class="card-body">
                                    <div class="row no-gutters align-items-center">
                                        <div class="col mr-2">
                                            <div class="text-xs font-weight-bold text-success text-uppercase mb-1">
                                                Total Profit Percentage</div>
                                                <asp:Label Id="Label2" runat="server" class="h5 mb-0 font-weight-bold text-gray-800"></asp:Label>
                                        </div>
                                        <div class="col-auto">
                                            <i class="fas fa-dollar-sign fa-2x text-gray-300"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

    </div>

        <div class="card shadow mb-6">

            <div class="card-header py-3">
                <h6 class="m-0 font-weight-bold text-primary">Projects & Allocations</h6>
            </div>

            <div runat="server">
                <asp:GridView ID="grd_projects"  HeaderStyle-BackColor= "#0099ff" HeaderStyle-Font-Bold="true" 
                    HeaderStyle-ForeColor="White" FooterStyle-Font-Bold="true"
                    runat="server" class="table dataTable"
                     AutoGenerateColumns ="false" OnRowDataBound="grd_projects_RowDataBound">
                     <Columns>

                         <asp:boundfield datafield="project_id" convertemptystringtonull="true" headertext="Project Id"/>

                         <asp:TemplateField HeaderText="Project Informations" ControlStyle-Font-Bold="true" ControlStyle-BorderColor="#ffffff">
                             <ItemTemplate>
                                 <table border="0">
                                     <tr><td runat="server" > <%#Eval("project_name")%></td></tr>
                                     <tr><td runat="server" > Status       :  <%#Eval("project_status")%></td></tr>
                                     <tr><td runat="server" > Duration     :  <%#Eval("project_duration")%></td></tr>
                                 </table>
                             </ItemTemplate>
                         </asp:TemplateField>   

                         <asp:boundfield datafield="amount_approved" convertemptystringtonull="true" headertext="Approved"/>
                         <asp:boundfield datafield="investors_contributed" convertemptystringtonull="true" headertext="Allocated"/>
                                       
                         <asp:TemplateField HeaderText="Approx Project Start & End" ControlStyle-Font-Bold="true" ControlStyle-BorderColor="#ffffff">
                             <ItemTemplate>
                                 <table border="0">
                                     <tr><td runat="server" > <%#Eval("StartDate")%></td></tr>
                                     <tr><td runat="server" > <%#Eval("EndDate")%></td></tr>
                                 </table>
                             </ItemTemplate>
                         </asp:TemplateField>    
                         
                         <asp:boundfield datafield="AdminCharge" convertemptystringtonull="true" headertext="AdminCharge"/>
                         <asp:boundfield datafield="OtherCharge" convertemptystringtonull="true" headertext="OtherCharge"/>
                         <asp:boundfield datafield="final_profit_amount" convertemptystringtonull="true" headertext="Final Profit"/>
                         <asp:boundfield datafield="profit_percentage" convertemptystringtonull="true" headertext="Profit %"/>
                         
                         <asp:TemplateField HeaderText="Investor & Allocation">
                             <ItemTemplate>
                                 <asp:GridView ID="grd_inner" runat="server" class="table dataTable" GridLines="None"> 
                                 </asp:GridView>
                             </ItemTemplate>
                         </asp:TemplateField>

                     </Columns>
                </asp:GridView>
                
            </div>
        </div>

</div>
</form>
<!--End of Page Content asraf>





            </div>
            <!-- End of Main Content -->
        </div>
        <!-- End of Content Wrapper -->

    </div>
    <!-- End of Page Wrapper -->

    <!-- Scroll to Top Button-->
    <a class="scroll-to-top rounded" href="#page-top">
        <i class="fas fa-angle-up"></i>
    </a>

    <!-- Logout Modal-->
    <div class="modal fade" id="logoutModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
        aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Ready to Leave?</h5>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">Select "Logout" below if you are ready to end your current session.</div>
                <div class="modal-footer">
                    <button class="btn btn-secondary" type="button" data-dismiss="modal">Cancel</button>
                    <a class="btn btn-primary" href="login.html">Logout</a>
                </div>
            </div>
        </div>
    </div>

    <!-- Bootstrap core JavaScript-->
    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

    <!-- Core plugin JavaScript-->
    <script src="vendor/jquery-easing/jquery.easing.min.js"></script>   

    <!-- Custom scripts for all pages-->
    <script src="js/sb-admin-2.min.js"></script>

    <!-- Page level plugins -->
    <script src="vendor/chart.js/Chart.min.js"></script>

    <!-- Page level custom scripts -->
    <script src="js/demo/chart-area-demo.js"></script>
    <script src="js/demo/chart-pie-demo.js"></script>

</body>