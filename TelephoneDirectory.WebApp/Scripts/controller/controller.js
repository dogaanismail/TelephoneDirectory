//Menu Controller
app.controller('MenuCtrl', function ($scope, $http) {
    //Get login user role
    $scope.init = function () {
        //Get role vise menu for display
        $http.get("/api/Menu",).then(function (result) {
            $scope.lstMenu = result.data;
            $scope.lstParentMenu = [];
            $scope.lstSubMenu = [];
            console.log(result);

            //make a main menu and a sub menu different list
            for (var i = 0; i < result.data.length; i++) {
                if (result.data[i].ParentModuleID === 0) {
                    $scope.lstParentMenu.push(result.data[i]);
                }
                else {
                    $scope.lstSubMenu.push(result.data[i]);
                }
            }

            setTimeout(function () {
                $scope.$apply(function () {
                    //Set submenu list in main menu 
                    for (var j = 0; j < $scope.lstParentMenu.length; j++) {
                        $scope.lstParentMenu[j].Submenu = [];

                        for (var k = 0; k < $scope.lstSubMenu.length; k++) {
                            if ($scope.lstParentMenu[j].ID === $scope.lstSubMenu[k].ParentModuleID) {
                                $scope.lstParentMenu[j].Submenu.push($scope.lstSubMenu[k]);
                                console.log($scope.lstSubMenu[k]);
                            }
                        }
                    }
                });
            }, 500);

        });
    };
    $scope.init();
});

//Employee Controller
app.controller('EmployeeCtrl', function ($scope, $http) {
    //Initialize data
    $scope.init = function () {
        $scope.model = {
            EmployeeID: '',
            FirstName: '',
            LastName: '',
            Phone_Number: '',
            ManagerID: '',
            DepartmentID: '',     
            DepartmentName: '', 
            ManagerName: ''
        };

        //for Employee Detail
        $scope.flgEmployeeDetail = false;

        //for Employee list
        $scope.flgTable = true;

        //for display message of user   
        $scope.flgMessage = false;

        $scope.flgEmployee = true;
        $scope.showCreate = false;
        $scope.showEdit = false;

        //for showing message
        $scope.flgMessage1 = false;
        $scope.message = "";
        $scope.message1 = "";

        //for Employee link
        $scope.getAllEmployee();
        $scope.EmployeeState = "";
        $scope.getAllDepartments();
        $scope.getEmploys();
    };

    //hide message
    $scope.hideMessage = function () {
        //make message flg false for hide message
        $scope.flgMessage = false;
        $("#message").removeClass("alert alert-success").removeClass("alert alert-danger");
        $("#icon").removeClass("fa-check").removeClass("fa-ban");

        $scope.flgMessage1 = false;
        $("#message1").removeClass("alert alert-success").removeClass("alert alert-danger");
        $("#icon1").removeClass("fa-check").removeClass("fa-ban");
    };

    //GetAllEmployee
    $scope.getAllEmployee = function () {
        var table = $("#employeeTable").DataTable();
        table.clear();
        table.destroy();
        $http({
            method: 'GET',
            url: '/api/Employees'
        }).then(function (result) {
            $scope.lstEmployees = result.data;

            setTimeout(function () {
                $('#employeeTable').DataTable({
                    "aoColumnDefs": [{
                        "bSortable": false,
                        "aTargets": [-1]
                    }],
                    "paging": true,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": true,
                    "autoWidth": false
                });
            }, 500);

        });

    };
 
    //Open employee form
    $scope.addEmployee = function () {
        //make table flg false for display message
        $scope.flgTable = false;
        $scope.showCreate = true;
        $scope.showEdit = false;
        $scope.EmployeeState = "> Add an employee";
    };

    //Edit Employee
    $scope.editEmployee = function (obj) {
        $scope.model.EmployeeID = obj.EmployeeID;
        $scope.model.FirstName = obj.FirstName;
        $scope.model.LastName = obj.LastName;
        $scope.model.Phone_Number = obj.Phone_Number;
        $scope.model.ManagerID = obj.ManagerID;
        $scope.model.DepartmentID = obj.DepartmentID;
        $scope.model.DepartmentName = obj.DepartmentName;
        $scope.flgTable = false;
        $scope.showCreate = false;
        $scope.showEdit = true;
    };

    //Create/update Employee
    $scope.createEmployee = function (obj) {
        $scope.hideMessage();
            $http.post("/api/Employees", obj).then(function (result) {
                if (result.data.success === 1) {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-success");
                    $("#icon").addClass("glyphicon glyphicon-ok");
                    $scope.getAllEmployee();
                    $scope.reset();
                }
                else {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-danger");
                    $("#icon").addClass("glyphicon glyphicon-warning-sign ");
                }
            });
    };

    //Delete Employee
    $scope.deleteEmployee = function (obj) {
        $scope.hideMessage();
        params = {
            id: obj.EmployeeID
        }
        $http.delete("/api/Employees", { params: params }).then(function (result) {
            if (result.data.success === 1) {
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-success");
                $("#icon").addClass("glyphicon glyphicon-ok");
                $scope.getAllEmployee();
                $scope.reset();
            }
            else {
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-danger");
                $("#icon").addClass("glyphicon glyphicon-warning-sign");
            }
        });
    };

    //Get all departments
    $scope.getAllDepartments = function () {
        $http.get("/api/Departments").then(function (result) {
            $scope.lstDepartments = result.data;
        });
    };

    //Get all departments
    $scope.getEmploys = function () {
        $http.get("/api/Employees").then(function (result) {
            $scope.lstEmployeeList = result.data;
        });
    };

    function formatDate(d) {
        date = new Date(d);
        var dd = date.getDate();
        var mm = date.getMonth() + 1;
        var yyyy = date.getFullYear();
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm };
        return d = yyyy + '-' + mm + '-' + dd;
    }

    //Reset data
    $scope.reset = function () {
        $scope.EmployeeState = "";
        $scope.flgTable = true;
        $scope.flgEmployeeDetail = false;
        $scope.flgEmployee = true;
        $scope.model = {
            EmployeeID: '',
            FirstName: '',
            LastName: '',
            Phone_Number: '',
            ManagerID: '',
            DepartmentID: '',
            DepartmentName: '',
            ManagerName:''
        },
        $("#liTab_2").removeClass("active");
        $("#tab_2").removeClass("active");
        $("#liTab_1").addClass("active");
        $("#tab_1").addClass("active");
    };
    $scope.init();
});

//Application User Controller
app.controller('UserCtrl', function ($scope, $http) {
    //Initialize data
    $scope.init = function () {
        $scope.model = {
            Id: '',
            Email: '',
            EmailConfirmed: '',
            PasswordHash: '',
            SecurityStamp: '',
            PhoneNumber: '',
            PhoneNumberConfirmed: '',
            TwoFactorEnabled: '',
            LockoutEndDateUtc: '',
            LockoutEnabled: '',
            AccessFailedCount: '',
            UserName: '',
            RoleID:''
           
        };

        //for user Detail
        $scope.flgUserDetail = false;

        //for user list
        $scope.flgTable = true;

        //for display message of user   
        $scope.flgMessage = false;

        $scope.flgUser = true;
        $scope.showCreate = false;
        $scope.showEdit = false;

        //for showing message
        $scope.flgMessage1 = false;
        $scope.message = "";
        $scope.message1 = "";

        $scope.UserState = "";
        $scope.getAllUser();
        $scope.getAllRoles();
    };

    //hide message
    $scope.hideMessage = function () {
        //make message flg false for hide message
        $scope.flgMessage = false;
        $("#message").removeClass("alert alert-success").removeClass("alert alert-danger");
        $("#icon").removeClass("fa-check").removeClass("fa-ban");

        $scope.flgMessage1 = false;
        $("#message1").removeClass("alert alert-success").removeClass("alert alert-danger");
        $("#icon1").removeClass("fa-check").removeClass("fa-ban");
    };

    //GetAllEmployee
    $scope.getAllUser = function () {
        var table = $("#userTable").DataTable();
        table.clear();
        table.destroy();
        $http({
            method: 'GET',
            url: '/api/Users'
        }).then(function (result) {
            $scope.lstUsers = result.data;

            setTimeout(function () {
                $('#userTable').DataTable({
                    "aoColumnDefs": [{
                        "bSortable": false,
                        "aTargets": [-1]
                    }],
                    "paging": true,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": true,
                    "autoWidth": false
                });
            }, 500);

        });

    };

    //Open user form
    $scope.addUser = function () {
        //make table flg false for display message
        $scope.flgTable = false;
        $scope.showCreate = true;
        $scope.showEdit = false;
        $scope.UserState = "> Add an user";
    };

    //Edit Users
    $scope.editUser = function (obj) {
        $scope.model.Id = obj.Id;
        $scope.model.Email = obj.Email;
        $scope.model.EmailConfirmed = obj.EmailConfirmed;
        $scope.model.PasswordHash = obj.PasswordHash;
        $scope.model.SecurityStamp = obj.SecurityStamp;
        $scope.model.PhoneNumber = obj.PhoneNumber;
        $scope.model.PhoneNumberConfirmed = obj.PhoneNumberConfirmed;
        $scope.model.TwoFactorEnabled = obj.TwoFactorEnabled;
        $scope.model.SecurityStamp = obj.SecurityStamp;
        $scope.model.LockoutEndDateUtc = obj.LockoutEndDateUtc;
        $scope.model.LockoutEnabled = obj.LockoutEnabled;
        $scope.model.UserName = obj.UserName;
        $scope.model.RoleID = obj.RoleID;

        $scope.flgTable = false;
        $scope.showCreate = false;
        $scope.showEdit = true;
    };

    //Create/update Usesr
    $scope.createUser = function (obj) {
        $scope.hideMessage();

        if ($scope.showCreate === true) {
            //Check password and confirm password is same
            if (obj.PasswordHash !== obj.CPassword) {
                $scope.flgMessage = true;
                $scope.message = "Password and Confirm Password must be same.";
                $("#message").addClass("alert alert-danger");
                $("#icon").addClass("glyphicon glyphicon-warning-sign");
            }
            else {
                $http.post("/api/Users", obj).then(function (result) {
                    if (result.data.success === 1) {
                        $scope.flgMessage = true;
                        $scope.message = result.data.message;
                        $("#message").addClass("alert alert-success");
                        $("#icon").addClass("glyphicon glyphicon-ok");
                        $scope.getAllUser();
                        $scope.reset();
                    }
                    else {
                        $scope.flgMessage = true;
                        $scope.message = result.data.message;
                        $("#message").addClass("alert alert-danger");
                        $("#icon").addClass("glyphicon glyphicon-warning-sign ");
                    }
                });
            }
        }
        else {
            $http.post("/api/Users", obj).then(function (result) {
                if (result.data.success === 1) {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-success");
                    $("#icon").addClass("glyphicon glyphicon-ok");
                    $scope.getAllUser();
                    $scope.reset();
                }
                else {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-danger");
                    $("#icon").addClass("glyphicon glyphicon-warning-sign ");
                }
            });
        }

    };

    //Delete User
    $scope.deleteUser = function (obj) {
        $scope.hideMessage();
        params = {
            id: obj.Id
        }
        $http.delete("/api/Users", { params: params }).then(function (result) {
            if (result.data.success === 1) {
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-success");
                $("#icon").addClass("glyphicon glyphicon-ok");
                $scope.getAllEmployee();
                $scope.reset();
            }
            else {
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-danger");
                $("#icon").addClass("glyphicon glyphicon-warning-sign");
            }
        });
    };

    //Get all roles
    $scope.getAllRoles = function () {
        $http.get("/api/Roles").then(function (result) {
            $scope.lstRoles = result.data;
        });
    };

    function formatDate(d) {
        date = new Date(d);
        var dd = date.getDate();
        var mm = date.getMonth() + 1;
        var yyyy = date.getFullYear();
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm };
        return d = yyyy + '-' + mm + '-' + dd;
    }

    //Reset data
    $scope.reset = function () {
        $scope.EmployeeState = "";
        $scope.flgTable = true;
        $scope.flgUserDetail = false;
        $scope.flgUser = true;
        $scope.model = {
            Id: '',
            Email: '',
            EmailConfirmed: '',
            PasswordHash: '',
            SecurityStamp: '',
            PhoneNumber: '',
            PhoneNumberConfirmed: '',
            TwoFactorEnabled: '',
            LockoutEndDateUtc: '',
            LockoutEnabled: '',
            AccessFailedCount: '',
            UserName: '',
            RoleID: ''
        },
            $("#liTab_2").removeClass("active");
        $("#tab_2").removeClass("active");
        $("#liTab_1").addClass("active");
        $("#tab_1").addClass("active");
    };
    $scope.init();
});

//Application Role Controller
app.controller('RoleCtrl', function ($scope, $http) {
    //Initialize data
    $scope.init = function () {

        $scope.model = {
            RoleID: '',
            RoleName: '',
        };

        //for Display Role list
        $scope.flgTable = true;

        //for Display Message
        $scope.flgMessage = false;

        //for message
        $scope.message = "";

        //for Role Link
        $scope.UserState = "";
        $scope.getAllRoles();
    };

    //Hide alert message
    $scope.hideMessage = function () {
        //make message flg false for hide message
        $scope.flgMessage = false;
        $("#message").removeClass("alert alert-success").removeClass("alert alert-danger");
        $("#icon").removeClass("fa-check").removeClass("fa-ban");
    };

    //Get all roles
    $scope.getAllRoles = function () {
        //Define table as Datatable
        var table = $("#roleTable").DataTable();
        //Clear table
        table.clear();
        //Destroy table
        table.destroy();

        $http.get("/api/Roles").then(function (result) {
            $scope.lstRoles = result.data;

            //Set table Configuration
            setTimeout(function () {
                $("#roleTable").DataTable({
                    "aoColumnDefs": [{
                        "bSortable": false,
                        "aTargets": [-1]
                    }],
                    "paging": true,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": true,
                    "autoWidth": false
                });
            }, 500);
        });
    };

    //Change flag for dispaly form
    $scope.addRole = function () {
        //Make Table flg false for display Add role form
        $scope.flgTable = false;
        //for Display Link
        $scope.UserState = "> Add Role";
    };

    //Edit role
    $scope.editRole = function (obj) {
        $scope.model.RoleID = obj.RoleID;
        $scope.model.RoleName = obj.RoleName;
        //For display role form
        $scope.flgTable = false;
        $scope.UserState = "> Edit Role";
    };

    //Create role
    $scope.createRole = function (obj) {
        $scope.hideMessage();
        $http.post("/api/Roles", obj).then(function (result) {
            if (result.data.success === 1) {
                //made message flag true for display message and add classes
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-success");
                $("#icon").addClass("glyphicon glyphicon-ok");
                $scope.getAllRoles();
                $scope.reset();
            }
            else {
                //made message flag true for display message and add classes
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-danger");
                $("#icon").addClass("glyphicon glyphicon-warning-sign");
            }
        });
    };

    //Delete role
    $scope.deleteRole = function (obj) {
        $scope.hideMessage();
        params = {
            id: obj.RoleID
        };
        $http.delete("/api/Roles", { params: params }).then(function (result) {
            if (result.data.success === 1) {
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-success");
                $("#icon").addClass("glyphicon glyphicon-ok");
                $scope.getAllRoles();
            }
            else {
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-danger");
                $("#icon").addClass("glyphicon glyphicon-warning-sign");
            }
        });
    };

    //Reset model
    $scope.reset = function () {
        $scope.flgTable = true;
        $scope.UserState = "";
        //$scope.flgMessage = false;
        $scope.model = {
            RoleID: '',
            RoleName: '',
        };
        $("#liTab_2").removeClass("active");
        $("#tab_2").removeClass("active");
        $("#liTab_1").addClass("active");
        $("#tab_1").addClass("active");
    };

    $scope.init();
});

//Module Controller
app.controller('ModuleCtrl', function ($scope, $http) {

    //Initialize data
    $scope.init = function () {
        $scope.model = {
            ID: '',
            DisplayOrder: '',
            ModuleName: '',
            PageIcon: '',
            PageUrl: '',
            ParentModuleID: 0
        };

        //for module link
        $scope.flgTable = true;
        //for display message
        $scope.flgMessage = false;
        //for message
        $scope.message = "";
        //for User link
        $scope.UserState = "";

        $scope.getAllModule();
    };

    //Hide alert message
    $scope.hideMessage = function () {
        //make message flg false for hide message
        $scope.flgMessage = false;
        $("#message").removeClass("alert alert-success").removeClass("alert alert-danger");
        $("#icon").removeClass("fa-check").removeClass("fa-ban");
    };

    //Get all module
    $scope.getAllModule = function () {
        //Define DataTable
        var table = $("#moduleTable").DataTable();
        table.clear();
        table.destroy();

        $http.get("/api/Modules").then(function (result) {
            $scope.lstModule = result.data;

            //Set Table Configuration
            setTimeout(function () {
                $('#moduleTable').DataTable({
                    "aoColumnDefs": [{
                        "bSortable": false,
                        "aTargets": [-1]
                    }],
                    "paging": true,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": true,
                    "autoWidth": false
                });
            }, 500);
        });
    };

    //Open module form
    $scope.addModule = function () {
        //make table flg false for Display Add Module form
        $scope.flgTable = false;
        $scope.UserState = "> Add Module";
    };

    //Edit module data
    $scope.editModule = function (obj) {
        //Edit Module
        $scope.UserState = "> Edit Module";
        $scope.model.ID = obj.ID;
        $scope.model.DisplayOrder = obj.DisplayOrder;
        $scope.model.ModuleName = obj.ModuleName;
        $scope.model.PageIcon = obj.PageIcon;
        $scope.model.PageUrl = obj.PageUrl;
        $scope.model.ParentModuleID = obj.ParentModuleID;
        $scope.flgTable = false;
    };

    //Create/update module
    $scope.createModule = function (obj) {
        $scope.hideMessage();
        $http.post("/api/Modules", obj).then(function (result) {
            if (result.data.success === 1) {
                //Make message flg true for display message
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-success");
                $("#icon").addClass("glyphicon glyphicon-ok");
                $scope.getAllModule();
                $scope.reset();
            }
            else {
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-danger");
                $("#icon").addClass("glyphicon glyphicon-warning-sign");
            }
        });
    };

    //Delete Module
    $scope.deleteModule = function (obj) {
        $scope.hideMessage();
        params = {
            id: obj.ID
        };
        $http.delete("/api/Modules", { params: params }).then(function (result) {
            if (result.data.success === 1) {
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-success");
                $("#icon").addClass("glyphicon glyphicon-ok");
                $scope.getAllModule();
            }
            else {
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-danger");
                $("#icon").addClass("glyphicon glyphicon-warning-sign");
            }
        });
    };

    //Reset data
    $scope.reset = function () {
        $scope.flgTable = true;
        $scope.UserState = "";
        $scope.model = {
            ID: '',
            DisplayOrder: '',
            ModuleName: '',
            PageIcon: '',
            PageUrl: '',
            ParentModuleID: 0
        };
    };

    $scope.init();
});

//Department Controller
app.controller('DepartmentCtrl', function ($scope, $http) {

    //Initialize data
    $scope.init = function () {
        $scope.model = {
            DepartmentID: '',
            DepartmentName: '',
            ManagerID: '',           
        };

        //for department link
        $scope.flgTable = true;
        //for display message
        $scope.flgMessage = false;
        //for message
        $scope.message = "";
        //for User link
        $scope.UserState = "";
        $scope.getAllDepartments();
        $scope.getAllEmployees();
    };

    //Hide alert message
    $scope.hideMessage = function () {
        //make message flg false for hide message
        $scope.flgMessage = false;
        $("#message").removeClass("alert alert-success").removeClass("alert alert-danger");
        $("#icon").removeClass("fa-check").removeClass("fa-ban");
    };

    //Get all departments
    $scope.getAllDepartments = function () {
        //Define DataTable
        var table = $("#departmentTable").DataTable();
        table.clear();
        table.destroy();

        $http.get("/api/Departments").then(function (result) {
            $scope.lstDepartment = result.data;

            //Set Table Configuration
            setTimeout(function () {
                $('#departmentTable').DataTable({
                    "aoColumnDefs": [{
                        "bSortable": false,
                        "aTargets": [-1]
                    }],
                    "paging": true,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": true,
                    "autoWidth": false
                });
            }, 500);
        });
    };

    //Get all employee
    $scope.getAllEmployees = function () {
        $http.get("/api/Employees").then(function (result) {
            $scope.lstManager = result.data;
        });
    };

   

    //Open department form
    $scope.addDepartment = function () {
        //make table flg false for Display Add Department form
        $scope.flgTable = false;
        $scope.UserState = "> Add Department";
    };

    //Edit department data
    $scope.editDepartment = function (obj) {
        //Edit Department
        $scope.UserState = "> Edit Department";

        $scope.model.DepartmentID = obj.DepartmentID;
        $scope.model.DepartmentName = obj.DepartmentName;
        $scope.model.ManagerID = obj.ManagerID;       
        $scope.flgTable = false;
    };

    //Create/update department
    $scope.createDepartment = function (obj) {
        $scope.hideMessage();
        $http.post("/api/Departments", obj).then(function (result) {
            if (result.data.success === 1) {
                //Make message flg true for display message
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-success");
                $("#icon").addClass("glyphicon glyphicon-ok");
                $scope.getAllDepartments();
                $scope.reset();
            }
            else {
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-danger");
                $("#icon").addClass("glyphicon glyphicon-warning-sign");
            }
        });
    };

    //Delete Department
    $scope.deleteDepartment = function (obj) {
        $scope.hideMessage();
        params = {
            id: obj.DepartmentID
        };
        $http.delete("/api/Departments", { params: params }).then(function (result) {
            if (result.data.success === 1) {
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-success");
                $("#icon").addClass("glyphicon glyphicon-ok");
                $scope.getAllDepartments();
            }
            else {
                $scope.flgMessage = true;
                $scope.message = result.data.message;
                $("#message").addClass("alert alert-danger");
                $("#icon").addClass("glyphicon glyphicon-warning-sign");
            }
        });
    };

    //Reset data
    $scope.reset = function () {
        $scope.flgTable = true;
        $scope.UserState = "";
        $scope.model = {
            DepartmentID: '',
            DepartmentName: '',
            ManagerID: '', 
        };
    };

    $scope.init();
});

//Dashboard Controller
app.controller('DashboardCtrl', function ($scope, $http) {

    //Initialize data
    $scope.init = function () {
        $scope.model = {
            TotalEmployees: 0,
            TotalUsers: 0,
            TotalDepartments: 0,
            TotalRoles: 0,
        };
        //Get Dashboard data
        $http.get("/api/DashboardData").then(function (result) {
            $scope.model.TotalEmployees = result.data.TotalEmployees;
            $scope.model.TotalUsers = result.data.TotalUsers;
            $scope.model.TotalDepartments = result.data.TotalDepartments;
            $scope.model.TotalRoles = result.data.TotalRoles;
        });
    };
    $scope.init();
});


