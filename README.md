# MarketMatrix
1. Installation Guide of Project:
2. Download the zip file and extract all
3. Move MarketMatrix_DataAccess, MarketMatrix_Models, MarketMatrix_Utility project outside of E-commerce-Project-ASPNET-Core-master folder (shown in below image)
4. ![Screenshot (244)](https://github.com/user-attachments/assets/de2e4580-1b6e-4089-8b33-8ee3c8214b0d)

5. Open  E-commerce-Project-ASPNET-Core-master folder -> MarketMatrix.sln (thus project will be opened and u must have visual studio editor)

# Update appsettings.json file
![Screenshot (245)](https://github.com/user-attachments/assets/76765fad-dd36-4915-b02a-eb671fc9d83f)  
-> in this define database connection url, which will look like this:  
  -> Server=LAPTOP-0HQRNHSO\\SQLEXPRESS;Database=marketmatrix;Trusted_Connection=True;TrustServerCertificate=True (update this according to your server name in SSMS)
-> In EmailSettings, define your gmail and gmail-app password for sending email used for password reset.


# Introduction of Project:-
MarketMatrix is a online shopping project built with ASP.NET Core MVC. This web application provides a good user experience through a frontend developed with HTML, Bootstrap, JavaScript, and AJAX.

The backend used ASP.NET Core MVC for clean, structured code with a repository layer, Entity Framwork for efficient database operations using SQL Server, and Identity Framework to protect user data with authentication and role-based authorization.

Features (User side):-<br>
✅ User authentication (Login/Register)<br>
✅ Product listing & search<br>
✅ Add to cart and place order<br>
✅ View order and cancel order<br>
✅ Forgor Password using email<br>

Features (Admin side):-<br>
✅ Admin authentication (Login)<br>
✅ Add category and Product<br>
✅ Update Order Status (Pending, Delivered, Rejected)<br>
✅ View Customer Details<br>

# Technologies Used:-  
ASP.NET Core MVC Architecture (Backend) (.NET 7.0 version)<br>
HTML | Bootstrap | AJAX (Frontend)<br>
SQL Server (Database)<br>
ASP.NET Entity Framework (Simplified database management)<br>
ASP.NET Identity Framework (Role based Authentication and Authorization)<br>
IEmail Sender (for passoword reset)<br>
Visual Studio 2022 Editor


