### Project Endpoints
![image](https://github.com/user-attachments/assets/b0c49f1d-33b2-4af0-be2c-e8135c3dba08)

### Project files
![image](https://github.com/user-attachments/assets/98b8aee3-d220-4630-b844-65d6710d428d)

## Invoice endpoints
### Schema of getting an invoice data with its items
![image](https://github.com/user-attachments/assets/36d1aeaa-7343-4b98-b33c-26dd51f2487d)

### Schema of adding new invoice
note: adding the cashier id will dynamically get the branch id he works in
![image](https://github.com/user-attachments/assets/933b0f0d-e8e0-48ff-951f-4e3a13f6d26d)

### Schema of updating an invoice and items of it
![image](https://github.com/user-attachments/assets/bb1357d0-b71b-47e6-9fed-556a2f9bec21)

-----
## Cashier
### Schema of getting cashier data
![image](https://github.com/user-attachments/assets/11009e63-7ede-4f94-a5f2-b6be5449d585)

### Schema of adding new cashier
![image](https://github.com/user-attachments/assets/8cb0d9fb-c85c-4470-b331-fe5767a8ddd2)

### Schema of updating new cashier
![image](https://github.com/user-attachments/assets/054b9883-34c0-4e8b-9d47-0565e68216d4)


-----

## How to run the application
- First download packages from NuGet Package Manager
  ![image](https://github.com/user-attachments/assets/e192e2a1-c17c-48d3-afb1-7c5a90509efa)

- Run in Package Manager console
  `Scaffold-DbContext "Server=localdb;Database=ShaTask;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models`

- In Front Make sure you are running on server `"http://localhost:5500" or "http://127.0.0.1:5500"` and using IIS Express

  











