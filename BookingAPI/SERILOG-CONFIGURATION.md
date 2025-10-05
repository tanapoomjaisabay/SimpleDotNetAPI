# Serilog File Retention Configuration

## Overview
This document explains the Serilog configuration for file logging with automatic retention and cleanup.

## Configuration Summary

### File Retention Settings

| Setting | Value | Description |
|---------|-------|-------------|
| **Max File Size** | 10 MB | Each log file can grow up to 10,485,760 bytes (10 MB) |
| **Max File Count** | 10 files | System keeps maximum of 10 log files |
| **File Lifetime** | 7 days | Files older than 7 days are automatically deleted |
| **Rolling Interval** | Daily | New file created each day |

## How It Works

### File Naming Convention
Log files are named using the pattern: `booking-api-YYYYMMDD-NNN.log`

Examples:
- `booking-api-20251005.log` - First file of the day
- `booking-api-20251005-001.log` - Second file (if first file reaches 10 MB)
- `booking-api-20251005-002.log` - Third file (if second file reaches 10 MB)

### Rolling Behavior

1. **Daily Rolling**: A new log file is created at midnight each day
2. **Size-Based Rolling**: If a file reaches 10 MB during the day:
   - Current file is closed
   - New file is created with incremented suffix (001, 002, etc.)
   - Logging continues in the new file

3. **Retention Cleanup**: Files are deleted when EITHER condition is met:
   - File is older than 7 days, OR
   - Total file count exceeds 10 files (oldest files deleted first)

### Performance Optimization

**Async Writing**: 
- Logs are written asynchronously to prevent blocking API requests
- Flush interval: 1 second (ensures logs are written to disk promptly)
- Buffer size: Default (optimized for performance)

## Example Scenarios

### Scenario 1: Low Traffic (< 10 MB per day)
```
Day 1: booking-api-20251001.log (5 MB)
Day 2: booking-api-20251002.log (6 MB)
Day 3: booking-api-20251003.log (4 MB)
...
Day 7: booking-api-20251007.log (5 MB)
Day 8: booking-api-20251008.log (7 MB) → Day 1 file deleted (7 days old)
```

### Scenario 2: High Traffic (> 10 MB per day)
```
Day 1: 
  - booking-api-20251001.log (10 MB) - filled up
  - booking-api-20251001-001.log (10 MB) - filled up
  - booking-api-20251001-002.log (3 MB) - current

Day 2:
  - booking-api-20251002.log (10 MB) - filled up
  - booking-api-20251002-001.log (8 MB) - current
  
...continues for 7 days with multiple files per day
```

### Scenario 3: Max File Count Reached
```
If you have:
- 3 files from Day 1
- 3 files from Day 2
- 3 files from Day 3
- 2 files from Day 4 (total = 11 files)

Result: Oldest file from Day 1 is deleted to maintain max 10 files
```

## Configuration Details

### appsettings.json
```json
{
  "Name": "Async",
  "Args": {
    "configure": [
      {
        "Name": "File",
        "Args": {
          "path": "logs/booking-api-.log",
          "rollingInterval": "Day",
          "fileSizeLimitBytes": 10485760,        // 10 MB
          "retainedFileCountLimit": 10,          // Max 10 files
          "retainedFileTimeLimit": "7.00:00:00", // 7 days
          "rollOnFileSizeLimit": true,           // Create new file when size limit reached
          "shared": false,                       // Single process writes to file
          "flushToDiskInterval": "00:00:01"      // Flush every 1 second
        }
      }
    ]
  }
}
```

### Key Configuration Parameters

| Parameter | Value | Purpose |
|-----------|-------|---------|
| `fileSizeLimitBytes` | 10485760 | 10 MB in bytes |
| `retainedFileCountLimit` | 10 | Maximum number of files to keep |
| `retainedFileTimeLimit` | "7.00:00:00" | TimeSpan format: 7 days |
| `rollOnFileSizeLimit` | true | Create new file when size limit is reached |
| `rollingInterval` | "Day" | Create new file each day at midnight |
| `flushToDiskInterval` | "00:00:01" | Write buffered logs every 1 second |
| `shared` | false | File is not shared between processes |

## Log Format

### Console Output
```
[22:11:56 INF] Starting BookingAPI application
```
Format: `[HH:mm:ss LEVEL] Message`

### File Output
```
[2025-10-05 22:11:56.123 +07:00] [INF] CorrelationId SourceContext Message
```
Format: `[Timestamp with timezone] [LEVEL] CorrelationId SourceContext Message`

## Monitoring & Maintenance

### Storage Calculation
Maximum disk usage (worst case):
- 10 files × 10 MB = **100 MB maximum**

Typical disk usage:
- Lower traffic: ~20-30 MB (few days of logs)
- Higher traffic: ~80-100 MB (approaching limits)

### Health Checks
Monitor log files in `logs/` directory:
```powershell
# List all log files with size
Get-ChildItem logs/*.log | Select-Object Name, Length, LastWriteTime

# Check total log directory size
(Get-ChildItem logs -File | Measure-Object -Property Length -Sum).Sum / 1MB
```

### Troubleshooting

**Issue: Files not being deleted**
- Check file system permissions on `logs/` directory
- Verify `retainedFileCountLimit` and `retainedFileTimeLimit` are set correctly

**Issue: Files rolling too frequently**
- Increase `fileSizeLimitBytes` if needed
- Check log level (reduce verbosity if too much logging)

**Issue: Performance degradation**
- Async writing should prevent this, but if it occurs:
  - Check disk I/O performance
  - Consider reducing `flushToDiskInterval` to 5-10 seconds

## Best Practices

1. ✅ **Keep async writing enabled** for production performance
2. ✅ **Monitor disk space** - Set up alerts at 80% usage
3. ✅ **Review retention settings** quarterly based on actual usage
4. ✅ **Archive important logs** before they're deleted (if needed for compliance)
5. ✅ **Use structured logging** with correlation IDs for better tracing
6. ✅ **Adjust log levels** per environment (verbose in Dev, less in Prod)

## Production Recommendations

For production environments, consider:

1. **Longer retention for audit logs**: Increase to 30-90 days if required
2. **External log aggregation**: Use ELK, Seq, or Application Insights
3. **Separate error logs**: Create dedicated sink for Error/Fatal levels
4. **Log shipping**: Archive to cloud storage (Azure Blob, S3) before deletion

## References

- [Serilog File Sink Documentation](https://github.com/serilog/serilog-sinks-file)
- [Serilog Async Sink Documentation](https://github.com/serilog/serilog-sinks-async)
- [ASP.NET Core Logging](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/)
