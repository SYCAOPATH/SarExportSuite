-- SarExportSuite Database Setup Script
-- Developer: sar
-- This script matches the user's existing database schema

-- Drop and recreate database (matches user's script)
IF DB_ID('SarExportSuiteDb') IS NOT NULL
BEGIN
    ALTER DATABASE SarExportSuiteDb SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE SarExportSuiteDb;
END
GO

CREATE DATABASE SarExportSuiteDb;
GO

USE SarExportSuiteDb;
GO

-- Create Exports Table (matches user's schema exactly)
CREATE TABLE Exports (
    ExportId INT IDENTITY(1,1) PRIMARY KEY,
    ExportName NVARCHAR(100),
    ExportDate DATETIME,
    Status NVARCHAR(50)
);
GO

-- Insert Sample Data (matches user's data exactly)
INSERT INTO Exports (ExportName, ExportDate, Status) VALUES
('Invoice - North Region', '2025-01-01 08:30:00', 'Completed'),
('Invoice - South Region', '2025-01-02 09:00:00', 'Pending'),
('Invoice - East Region', '2025-01-03 10:15:00', 'Completed'),
('Invoice - West Region', '2025-01-03 14:30:00', 'Failed'),
('Report - Q1 Summary', '2025-01-10 11:00:00', 'Completed'),
('Report - Q2 Summary', '2025-02-15 12:45:00', 'In Progress'),
('Shipment Export A', '2025-02-20 13:15:00', 'Completed'),
('Shipment Export B', '2025-02-25 16:00:00', 'Pending'),
('Client Data - Alpha', '2025-03-01 08:30:00', 'Failed'),
('Client Data - Beta', '2025-03-02 09:00:00', 'Completed'),
('Client Data - Gamma', '2025-03-03 10:15:00', 'In Progress'),
('Product Export - A1', '2025-03-05 14:30:00', 'Completed'),
('Product Export - B2', '2025-03-07 15:45:00', 'Pending'),
('Invoice - Central Region', '2025-03-10 16:30:00', 'Completed'),
('Internal Audit Export', '2025-03-15 09:00:00', 'Completed'),
('Monthly Report - Jan', '2025-03-18 10:30:00', 'Failed'),
('Monthly Report - Feb', '2025-03-21 13:00:00', 'In Progress'),
('Monthly Report - Mar', '2025-03-25 15:45:00', 'Completed'),
('Sales Export - North', '2025-04-01 09:00:00', 'Completed'),
('Sales Export - South', '2025-04-02 10:15:00', 'Pending'),
('Sales Export - East', '2025-04-03 11:30:00', 'Failed'),
('Sales Export - West', '2025-04-04 13:00:00', 'Completed'),
('Yearly Summary 2024', '2025-04-05 14:30:00', 'Completed'),
('Yearly Summary 2023', '2025-04-06 16:00:00', 'Failed'),
('Backup Export - Alpha', '2025-04-10 08:30:00', 'Completed'),
('Backup Export - Beta', '2025-04-11 09:15:00', 'Pending'),
('Backup Export - Gamma', '2025-04-12 10:30:00', 'In Progress'),
('Critical Export - Finance', '2025-04-15 11:45:00', 'Completed'),
('Critical Export - HR', '2025-04-16 13:00:00', 'Completed'),
('Critical Export - IT', '2025-04-17 14:15:00', 'Failed'),
('Special Export - Legacy', '2025-04-18 15:30:00', 'Completed'),
('Special Export - NewGen', '2025-04-19 16:45:00', 'In Progress'),
('Test Export - Dev', '2025-04-20 08:00:00', 'Pending'),
('Test Export - QA', '2025-04-21 09:30:00', 'Completed'),
('Test Export - Staging', '2025-04-22 11:00:00', 'Failed'),
('Test Export - Prod', '2025-04-23 12:30:00', 'Completed'),
('Customer Export - A', '2025-04-24 14:00:00', 'Pending'),
('Customer Export - B', '2025-04-25 15:30:00', 'Completed'),
('Customer Export - C', '2025-04-26 17:00:00', 'Completed'),
('Legacy Archive Export', '2025-04-27 18:30:00', 'Completed');
GO

-- Create GetExportSummary Stored Procedure with CTE (matches user's implementation)
CREATE PROCEDURE GetExportSummary
AS
BEGIN
    -- sar: CTE System as requested
    WITH ExportCTE AS (
        SELECT 
            ExportId,
            ExportName,
            ExportDate,
            Status
        FROM Exports
    )
    SELECT * FROM ExportCTE;
END;
GO

-- Verify the setup
PRINT 'Database setup completed successfully!';
PRINT 'Tables created: Exports';
PRINT 'Stored procedures created: GetExportSummary';
PRINT 'Sample data inserted: ' + CAST((SELECT COUNT(*) FROM Exports) AS NVARCHAR(10)) + ' records';
PRINT 'Developer: sar - SarExportSuite';

-- Test the stored procedure
PRINT 'Testing GetExportSummary stored procedure...';
EXEC GetExportSummary;
