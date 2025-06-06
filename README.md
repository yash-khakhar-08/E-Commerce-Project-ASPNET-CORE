# MarketMatrix
1. Installation Guide of Project:
2. Download the zip file and extract all
3. Move MarketMatrix_DataAccess, MarketMatrix_Models, MarketMatrix_Utility project outside of E-commerce-Project-ASPNET-Core-master folder (shown in below image)
4. ![Screenshot (244)](https://github.com/user-attachments/assets/de2e4580-1b6e-4089-8b33-8ee3c8214b0d)

5. Open  E-commerce-Project-ASPNET-Core-master folder -> MarketMatrix.sln (thus project will be opened and u must have visual studio editor)

# Update appsettings.json file
![Screenshot (245)](https://github.com/user-attachments/assets/76765fad-dd36-4915-b02a-eb671fc9d83f)  
-> in this define database connection url, which will look like this:<br>
  "ConnectionUrl": "Server=LAPTOP-0HQRNHSO\\SQLEXPRESS;Database=marketmatrix;Trusted_Connection=True;TrustServerCertificate=True" <br> (update this according to your server name in SSMS)<br>
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
Visual Studio 2022 Editor<br>

# Screenshots:-
1. Customer Side:-<br>

A. Landing Page:
![Screenshot (246)](https://github.com/user-attachments/assets/e7017b57-0274-4d67-81f9-3267f0c317f3)

B. Men Section:
![Screenshot (247)](https://github.com/user-attachments/assets/31797b0b-d5ca-488e-8311-f6a7405718e0)

C. Login Page:
![Screenshot (248)](https://github.com/user-attachments/assets/9dd58a3b-b43c-47ea-89a6-337de1245c15)

D. Register Page:
![Screenshot (249)](https://github.com/user-attachments/assets/92e2e5c2-4fb0-44dc-ab4c-2d487579b172)

E. Shopping Cart Page:
![Screenshot (250)](https://github.com/user-attachments/assets/b5dcd0ae-d84a-460e-a451-1f840086cf56)

F. Checkout Page:
![Screenshot (251)](https://github.com/user-attachments/assets/255308bd-b8eb-4dc3-8c53-c5e4cfa2a90c)

G. Profile Page:
![Screenshot (252)](https://github.com/user-attachments/assets/c2e86470-7897-4b5d-bca2-e328796c6187)


2. Admin Side:-<br>

A. Home Page:
![Screenshot (253)](https://github.com/user-attachments/assets/ff1ef165-916b-44e4-97fe-d704e5e5950e)

B. Categories List Page:
![Screenshot (254)](https://github.com/user-attachments/assets/f701aaf4-d788-4ec2-9886-507cd3fb2f3c)

C. Product List Page:
![Screenshot (255)](https://github.com/user-attachments/assets/99bc26b4-024c-4ca7-ad91-6617233a3fe3)

D. Pending Orders Page:
![Screenshot (256)](https://github.com/user-attachments/assets/f86aa41d-f2be-4f62-92cd-935442473881)

E. Customer Details Page:
![Screenshot (257)](https://github.com/user-attachments/assets/ec7a9dc3-fa94-4abf-817b-0451bcba7417)

Note: If any error comes while running the project, or gets difficulty in installation of project, contact me:<br>
Email: yashkhakhkhar455@gmail.com









