
# Crafty

An E-commerce website where customers can buy home-crafted items.


## Features

- User can Registration & Login into the site.
- The entire site will change according to the role, depending on whether the User is Customer or Admin.
- User can logout after completing.
- User can View Product information
- User can Select the product and add it to the cart to order
- If User Role is Admin, He Can Enter the Admin Dashboard
- The admin dashboard will display some of the relevant statistics of the website including Order list, Top Three Buyers, Top Three Popular Products, Revenue
- Admin can add, update the product
- Admin can see details of order list, product list, Payment list, Customer list
- Admin can manage the transaction process



## System Requirements

**Framework:** ASP.NET MVC

**Server:** MS SQL Server 2015 Express

**IDE:** Visual Studio 2022


## Installation

Install Visual Studio 2022 and MS SQL Server 2015 Express. Then Download the project From Github.

Change the Connection String Source and Password according to Your SQLEXPRESS
```bash
   <connectionStrings><add name="Crafty_DBEntities" connectionString="metadata=res://*/Models.Crafty_DBEntities.csdl|res://*/Models.Crafty_DBEntities.ssdl|res://*/Models.Crafty_DBEntities.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=RIWINZO\SQLEXPRESS;initial catalog=Crafty_DB;user id=sa;password=123456;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
   <add name="Crafty_DBEntities1" connectionString="metadata=res://*/Models.Crafty_DBEntities.csdl|res://*/Models.Crafty_DBEntities.ssdl|res://*/Models.Crafty_DBEntities.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=RIWINZO\SQLEXPRESS;initial catalog=Crafty_DB;user id=sa;password=123456;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" /></connectionStrings>
```
    
