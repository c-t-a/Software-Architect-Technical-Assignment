Software Architect Technical Coding test
=============

This is the coding test for 2C2CP.

Prerequisite
-----------
Before running the applicaiton, please make your that you need to change database connection setting in API > appsetting.json.

```
line 11 "ServerConnection": "Server=localhost;Port=5432;;User Id=(your user id);Password=(your user password);"
```

In the `Test Data` folder, there had some file that you can test for this assignment.

API URL
-----------

To get all the invoices list, please used `/api/invoices`
To get all the invoices info by currecny code, please used `/api/currecy/{your currency code}`
To get all the invoices list by status, please used `/api/status/{your status}`
