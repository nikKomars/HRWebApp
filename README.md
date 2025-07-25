# HRWebApp — Personāla Vadības Sistēma

Šis projekts ir pilna steka tīmekļa lietojumprogramma, kas ļauj pārvaldīt organizācijas struktūru un darbiniekus.

---

## 📦 Tehnoloģijas
- **Frontend**: React (JavaScript)
- **Backend**: .NET 8 Web API (C#)
- **Datubāze**: Oracle 21c
- **ORM**: Entity Framework Core
- **UI**: Bootstrap

---

## ⚙️ Projekta struktūra
HRWebApp/
│
├── Controllers/ // API kontrolieri (Employee, Category u.c.)
├── Models/ // Entītiju modeļi (Employee, Organization u.c.)
├── Data/ // AppDbContext
├── Migrations/ // EF Core migrācijas
├── ClientApp/ // React frontend
│ ├── views/
│ ├── components/
│ └── ...
├── appsettings.json // Konfigurācija (DB savienojums)
└── Program.cs // .NET galvenais fails



---

## 🛠️ Uzstādīšana (lokāli)

### 1. Klonēt repozitoriju

Terminal
git clone https://github.com/nikKomars/HRWebApp.git
cd HRWebApp


2. Backend (.NET + Oracle)
✅ Prasības:
.NET 8 SDK

Oracle 21c

Oracle lietotājs ar tiesībām izveidot tabulas

🧱 Komandas: //Atvērot projektu Visual Studio 2022 pēc klonēšanas ievadīt visas šīs komandas lai tiek izveidota datu bāze un varētu atvert swagger (ar f5 palīdzību) un lai crud operacijas sāktu strādāt ne tikai swagger bet arī no izveidotās React tīmekļa vietnes.

Terminal
Copy
Edit
dotnet restore
dotnet tool install --global dotnet-ef
dotnet ef database update
dotnet run

Datubāzes savienojums ir konfigurēts appsettings.json:
json
Copy
Edit
"ConnectionStrings": {
  "DefaultConnection": "User Id=hrapp;Password=your_password;Data Source=localhost:1521/ORCLPDB"
}

3. Frontend (React) //Atvert Visual Studio Code visu mapi HRCRUDAPP un terminala ievadīt cd ./cd ./my-hr-app. Pēc tam var ievadīt npm start un atversies React App izveidotā tīmekļa vietne. Bet Pirms tam ir jāatver swagger no API (visual studio 2022 uzspiēžot F5)
Terminal
Copy
Edit
cd ClientApp
npm install
npm start
