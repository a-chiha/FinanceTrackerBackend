@echo off
echo ========= FINANCE TRACKER DATABASE MIGRATION SETUP =========
echo.
echo This script will help you set up the initial migrations for your database.
echo Make sure you have Entity Framework Core tools installed:
echo dotnet tool install --global dotnet-ef
echo.
echo 1. Adding initial migration...
cd ..
dotnet ef migrations add InitialCreate --project FinanceTracker --startup-project FinanceTracker
echo.
echo 2. Verifying migration was created...
echo.
echo 3. To apply this migration to your database, run:
echo    dotnet ef database update --project FinanceTracker --startup-project FinanceTracker
echo.
echo 4. For Azure SQL Server, make sure to:
echo    - Allow your local IP address in the firewall rules
echo    - Allow Render's IP address (52.59.103.54) in the firewall rules
echo    - Consider enabling "Allow Azure services to access this server"
echo.
echo 5. After applying migrations locally, deploy to Render
echo.
echo ========= MIGRATION SETUP COMPLETE ========= 