# PersonManagementAPI
## 🔧 Installation and Setup

### 1️⃣ **Clone the Repository**
```bash
git clone https://github.com/bezzhu/PersonManagementAPI.git
cd PersonManagementAPI
```

### 2️⃣ **Configure the Database**
Edit the **appsettings.json** file and update the **Connection String** with your SQL Server credentials:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=PersonManagementDB;Trusted_Connection=True;"
}
```

### 3️⃣ **Apply Migrations and Create Database**
```bash
dotnet ef database update
```

### 4️⃣ **Run the Project**
```bash
dotnet run
```
