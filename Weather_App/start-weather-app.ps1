Write-Host "========================================" -ForegroundColor Cyan
Write-Host "   Weather App - Full Stack Application" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "This script will start both the backend API and frontend application." -ForegroundColor Yellow
Write-Host ""
Write-Host "Backend will run on: http://localhost:5000" -ForegroundColor Green
Write-Host "Frontend will run on: http://localhost:4200" -ForegroundColor Green
Write-Host ""
Write-Host "Press any key to start..." -ForegroundColor Yellow
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")

Write-Host ""
Write-Host "Starting backend API..." -ForegroundColor Green
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$PWD\WeatherApp.API'; dotnet run"

Write-Host ""
Write-Host "Waiting for backend to start..." -ForegroundColor Yellow
Start-Sleep -Seconds 5

Write-Host ""
Write-Host "Starting frontend application..." -ForegroundColor Green
Start-Process powershell -ArgumentList "-NoExit", "-ExecutionPolicy Bypass", "-Command", "cd '$PWD'; npm start"

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "   Applications are starting..." -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Backend API: http://localhost:5000" -ForegroundColor Green
Write-Host "Frontend App: http://localhost:4200" -ForegroundColor Green
Write-Host ""
Write-Host "Both applications will open in new PowerShell windows." -ForegroundColor Yellow
Write-Host "Close those windows to stop the applications." -ForegroundColor Yellow
Write-Host ""
Read-Host "Press Enter to exit"
