# 🧪 Test Search History Feature

Write-Host "🚀 Testing Search History & Analytics Feature" -ForegroundColor Cyan
Write-Host "============================================`n" -ForegroundColor Cyan

$baseUrl = "http://localhost:5107"

# Wait for user to start the application
Write-Host "⚠️  Make sure the API is running!" -ForegroundColor Yellow
Write-Host "   Run 'dotnet run' in another terminal or press F5 in VS Code`n" -ForegroundColor Yellow
Read-Host "Press Enter when the API is running..."

Write-Host "`n📝 Step 1: Making multiple flight searches..." -ForegroundColor Green
Write-Host "----------------------------------------------" -ForegroundColor Green

$searches = @(
    @{
        origin         = "JFK"
        destination    = "LAX"
        departureDate  = "2025-12-25"
        cabinClass     = "Economy"
        passengerCount = 2
    },
    @{
        origin         = "JFK"
        destination    = "LAX"
        departureDate  = "2025-12-26"
        cabinClass     = "Business"
        passengerCount = 1
    },
    @{
        origin         = "ORD"
        destination    = "MIA"
        departureDate  = "2025-12-20"
        cabinClass     = "Economy"
        passengerCount = 4
    },
    @{
        origin         = "JFK"
        destination    = "LAX"
        departureDate  = "2025-12-27"
        cabinClass     = "First"
        passengerCount = 3
    },
    @{
        origin         = "SFO"
        destination    = "NYC"
        departureDate  = "2025-12-15"
        cabinClass     = "Economy"
        passengerCount = 1
    }
)

$searchCount = 0
foreach ($search in $searches) {
    $searchCount++
    try {
        $body = $search | ConvertTo-Json
        $result = Invoke-RestMethod -Uri "$baseUrl/api/flight-fares/search" `
            -Method Post `
            -Body $body `
            -ContentType "application/json"
        
        Write-Host "  ✅ Search $searchCount`: $($search.origin) → $($search.destination)" -ForegroundColor Green
        Write-Host "     Cabin: $($search.cabinClass), Fares Found: $($result.Count)" -ForegroundColor Gray
        
        Start-Sleep -Milliseconds 500
    }
    catch {
        Write-Host "  ❌ Search $searchCount failed: $($_.Exception.Message)" -ForegroundColor Red
    }
}

Write-Host "`n📊 Step 2: Retrieving recent search history..." -ForegroundColor Green
Write-Host "----------------------------------------------" -ForegroundColor Green

try {
    $recent = Invoke-RestMethod -Uri "$baseUrl/api/analytics/recent-searches?limit=10" -Method Get
    
    Write-Host "  Total searches in database: $($recent.totalCount)`n" -ForegroundColor Cyan
    
    $recent.searches | ForEach-Object {
        Write-Host "  🔍 $($_.route)" -ForegroundColor White
        Write-Host "     Date: $($_.departureDate), Class: $($_.cabinClass)" -ForegroundColor Gray
        Write-Host "     Passengers: $($_.passengerCount), Fares Found: $($_.totalFaresFound)" -ForegroundColor Gray
        Write-Host "     Searched At: $($_.searchedAt)`n" -ForegroundColor DarkGray
    }
}
catch {
    Write-Host "  ❌ Failed to retrieve recent searches: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host "`n🔥 Step 3: Viewing popular routes..." -ForegroundColor Green
Write-Host "----------------------------------------------" -ForegroundColor Green

try {
    $popular = Invoke-RestMethod -Uri "$baseUrl/api/analytics/popular-routes?topCount=5" -Method Get
    
    Write-Host "  Top routes by search frequency:`n" -ForegroundColor Cyan
    
    $rank = 0
    $popular.routes | ForEach-Object {
        $rank++
        Write-Host "  $rank. $($_.route)" -ForegroundColor White
        Write-Host "     Searched $($_.searchCount) time(s)`n" -ForegroundColor Gray
    }
}
catch {
    Write-Host "  ❌ Failed to retrieve popular routes: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host "`n📈 Step 4: Viewing comprehensive statistics..." -ForegroundColor Green
Write-Host "----------------------------------------------" -ForegroundColor Green

try {
    $stats = Invoke-RestMethod -Uri "$baseUrl/api/analytics/statistics" -Method Get
    
    Write-Host "  Overall Statistics:" -ForegroundColor Cyan
    Write-Host "  • Total Searches: $($stats.totalSearches)" -ForegroundColor White
    Write-Host "  • Total Fares Returned: $($stats.totalFaresReturned)" -ForegroundColor White
    Write-Host "  • Average Fares Per Search: $([math]::Round($stats.averageFaresPerSearch, 2))" -ForegroundColor White
    
    if ($stats.mostPopularCabinClass) {
        Write-Host "  • Most Popular Cabin: $($stats.mostPopularCabinClass.cabinClass) ($($stats.mostPopularCabinClass.count) searches)" -ForegroundColor White
    }
    
    Write-Host "`n  Top 3 Routes:" -ForegroundColor Cyan
    $stats.topRoutes | Select-Object -First 3 | ForEach-Object {
        Write-Host "    - $($_.route): $($_.count) searches" -ForegroundColor Gray
    }
    
    Write-Host "`n  Recent Activity:" -ForegroundColor Cyan
    $stats.searchesByDate | Select-Object -First 5 | ForEach-Object {
        Write-Host "    - $($_.date): $($_.count) searches" -ForegroundColor Gray
    }
}
catch {
    Write-Host "  ❌ Failed to retrieve statistics: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host "`n✅ Test Complete!" -ForegroundColor Green
Write-Host "============================================" -ForegroundColor Cyan
Write-Host "`nℹ️  All search counts are saved in the mock database!" -ForegroundColor Yellow
Write-Host "   Try running this script again to see cumulative data.`n" -ForegroundColor Yellow

Write-Host "📖 For more information, see:" -ForegroundColor Cyan
Write-Host "   • SEARCH-HISTORY-IMPLEMENTATION.md" -ForegroundColor Gray
Write-Host "   • http://localhost:5107/swagger (API documentation)`n" -ForegroundColor Gray
