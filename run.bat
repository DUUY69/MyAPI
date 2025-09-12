@echo off
echo ========================================
echo    MyAPI Project - Quick Commands
echo ========================================
echo.
echo 1. Restore packages
echo 2. Build solution
echo 3. Run application
echo 4. Create migration
echo 5. Update database
echo 6. Open Swagger UI
echo 7. Exit
echo.
set /p choice="Choose an option (1-7): "

if "%choice%"=="1" (
    echo Restoring packages...
    dotnet restore
    pause
    goto :eof
)

if "%choice%"=="2" (
    echo Building solution...
    dotnet build
    pause
    goto :eof
)

if "%choice%"=="3" (
    echo Starting application...
    dotnet run --project MyAPI.WebApi
    pause
    goto :eof
)

if "%choice%"=="4" (
    set /p migrationName="Enter migration name: "
    echo Creating migration: %migrationName%
    dotnet ef migrations add %migrationName% --project MyAPI.Repositories --startup-project MyAPI.WebApi
    pause
    goto :eof
)

if "%choice%"=="5" (
    echo Updating database...
    dotnet ef database update --project MyAPI.Repositories --startup-project MyAPI.WebApi
    pause
    goto :eof
)

if "%choice%"=="6" (
    echo Opening Swagger UI...
    start https://localhost:7024/swagger
    pause
    goto :eof
)

if "%choice%"=="7" (
    echo Goodbye!
    exit
)

echo Invalid choice. Please try again.
pause
