select * from EmployeeLogin

USE [HRMS]
GO
/****** Object:  Database [HRMS]    Script Date: 11-06-2024 11:56:57 AM ******/
ALTER DATABASE [HRMS] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HRMS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HRMS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HRMS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HRMS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HRMS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HRMS] SET ARITHABORT OFF 
GO
ALTER DATABASE [HRMS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HRMS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HRMS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HRMS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HRMS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HRMS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HRMS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HRMS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HRMS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HRMS] SET  ENABLE_BROKER 
GO
ALTER DATABASE [HRMS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HRMS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HRMS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HRMS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HRMS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HRMS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HRMS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HRMS] SET RECOVERY FULL 
GO
ALTER DATABASE [HRMS] SET  MULTI_USER 
GO
ALTER DATABASE [HRMS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HRMS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HRMS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HRMS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HRMS] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HRMS] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'HRMS', N'ON'
GO
ALTER DATABASE [HRMS] SET QUERY_STORE = ON
GO
ALTER DATABASE [HRMS] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [HRMS]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](15) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Designations]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Designations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Designation] [varchar](25) NOT NULL,
 CONSTRAINT [PK_Designation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeDetails]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeCode]  AS ('BYT'+right('000'+CONVERT([varchar](10),[id]),(3))) PERSISTED,
	[FirstName] [varchar](40) NOT NULL,
	[MiddleName] [varchar](40) NULL,
	[LastName] [varchar](40) NOT NULL,
	[Email] [nvarchar](40) NOT NULL,
	[MobileNumber] [varchar](30) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[City] [varchar](60) NOT NULL,
	[PinCode] [varchar](10) NOT NULL,
	[State] [varchar](60) NOT NULL,
	[Country] [varchar](60) NOT NULL,
	[Role] [int] NOT NULL,
	[Designation] [int] NOT NULL,
	[Experience] [int] NOT NULL,
	[Gender] [varchar](10) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[DateOfJoining] [date] NULL,
	[Religion] [varchar](60) NOT NULL,
	[Nationality] [varchar](60) NOT NULL,
	[MaritalStatus] [varchar](60) NOT NULL,
	[ProfileImage] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [date] NOT NULL,
	[ModifiedDate] [date] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[Field1] [varchar](60) NULL,
	[Field2] [varchar](30) NULL,
	[Field3] [varchar](30) NULL,
 CONSTRAINT [PK_EmployeeDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeePersonalDetails]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeePersonalDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NULL,
	[PgCollegeName] [varchar](100) NULL,
	[PgStart] [date] NULL,
	[PgEnd] [date] NULL,
	[PgCourse] [varchar](60) NULL,
	[UgCollageName] [varchar](100) NULL,
	[UgStart] [date] NULL,
	[UgEnd] [date] NULL,
	[UgCourse] [varchar](60) NULL,
	[HSCSchoolName] [varchar](100) NULL,
	[HSCStart] [date] NULL,
	[HSCEnd] [date] NULL,
	[HSCCourse] [varchar](60) NULL,
	[SSLCSchoolName] [varchar](100) NOT NULL,
	[SSLCStart] [date] NULL,
	[SSLCEnd] [date] NULL,
	[EmergencyContact] [varchar](20) NOT NULL,
	[Relationship] [varchar](40) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [date] NOT NULL,
	[ModifiedDate] [date] NULL,
	[Field1] [varchar](30) NULL,
	[Field2] [varchar](30) NULL,
	[Field3] [varchar](30) NULL,
 CONSTRAINT [PK_EmployeePersonalDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeAccountDetails]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeAccountDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NULL,
	[salary] [decimal](11, 2) NULL,
	[BankName] [varchar](50) NOT NULL,
	[AccountNumber] [varchar](30) NOT NULL,
	[IFSCCode] [varchar](30) NOT NULL,
	[PFAccount] [varchar](30) NULL,
	[IsActive] [bit] NULL,
	[Status] [varchar](20) NULL,
	[CreatedDate] [date] NULL,
	[ModifiedDate] [date] NULL,
	[CreatedName] [varchar](30) NULL,
	[ModifiedName] [varchar](30) NULL,
	[EmployeeCode] [varchar](30) NULL,
	[Field2] [varchar](30) NULL,
	[Field3] [varchar](30) NULL,
 CONSTRAINT [PK_AccountDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[EmployeeBasicDetails]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[EmployeeBasicDetails] AS
SELECT  EmployeeDetails.[ID],
        EmployeeDetails.[EmployeeCode],
        EmployeeDetails.[FirstName],
        EmployeeDetails.[MiddleName],
        EmployeeDetails.[LastName],
        EmployeeDetails.[Email],
        EmployeeDetails.[MobileNumber],
        Designations.[Designation],
        Roles.[Role],
        EmployeeDetails.[Experience],
        EmployeeDetails.[DateOfJoining],
        EmployeeDetails.[DateOfBirth],
        EmployeeDetails.[Address],
        EmployeeDetails.[City],
        EmployeeDetails.[Country],
        EmployeeDetails.[State],
        EmployeeDetails.[PinCode],
        EmployeeDetails.[Gender],
        EmployeeDetails.[Religion],
        EmployeeDetails.[Nationality],
        EmployeeDetails.[MaritalStatus],
        EmployeeDetails.[ProfileImage],
        EmployeeDetails.[Designation] AS DesignationId,
        EmployeePersonalDetails.[PgCollegeName],
        EmployeePersonalDetails.[PgCourse],
        EmployeePersonalDetails.[PgStart],
        EmployeePersonalDetails.[PgEnd],
        EmployeePersonalDetails.[UgCollageName],
        EmployeePersonalDetails.[UgCourse],
        EmployeePersonalDetails.[UgStart],
        EmployeePersonalDetails.[UgEnd],
        EmployeePersonalDetails.[HSCSchoolName],
        EmployeePersonalDetails.[HSCCourse],
        EmployeePersonalDetails.[HSCStart],
        EmployeePersonalDetails.[HSCEnd],
        EmployeePersonalDetails.[SSLCSchoolName],
        EmployeePersonalDetails.[SSLCStart],
        EmployeePersonalDetails.[SSLCEnd],
        EmployeePersonalDetails.[EmergencyContact],
        EmployeePersonalDetails.[Relationship],
        EmployeeAccountDetails.[BankName],
        EmployeeAccountDetails.[salary],
        EmployeeAccountDetails.[AccountNumber],
        EmployeeAccountDetails.[IFSCCode],
         EmployeeAccountDetails.[PFAccount],
        EmployeeDetails.[IsActive],
        EmployeeAccountDetails.[Status]
      
FROM EmployeeDetails 
INNER JOIN EmployeePersonalDetails ON EmployeePersonalDetails.[EmpId]=EmployeeDetails.[ID]
INNER JOIN EmployeeAccountDetails ON EmployeeDetails.[ID]=EmployeeAccountDetails.[EmpID]
INNER JOIN Designations ON EmployeeDetails.[Designation]=Designations.[ID]
INNER JOIN Roles ON EmployeeDetails.[Role]=Roles.[ID]
WHERE EmployeeDetails.IsActive=1;
GO
/****** Object:  View [dbo].[DisplayEmployeeDetails]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   view [dbo].[DisplayEmployeeDetails] As 
select [ID],
        [EmployeeCode],
        CONCAT([FirstName],' ',COALESCE([MiddleName],' '),' ',[LastName]) AS EmployeeName,
        [Email],
        [Designation],
        [DateOfJoining],
        [salary]
        from EmployeeBasicDetails
GO
/****** Object:  Table [dbo].[Task]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NULL,
	[TaskName] [nvarchar](60) NULL,
	[DueDate] [date] NULL,
	[EmpId] [int] NULL,
	[Status] [bit] NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [date] NULL,
	[ModifiedDate] [date] NULL,
	[CompletedAt] [date] NULL,
 CONSTRAINT [PK_TaskID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[TaskSummary]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   VIEW [dbo].[TaskSummary] 
AS
SELECT 
    ProjectID,  TotalTasks, CompletedTasks, (CAST(CompletedTasks AS FLOAT) / TotalTasks) * 100  AS PercentageCompleted
FROM (
    SELECT 
        ProjectID,
        COUNT(TaskName) AS TotalTasks,
        SUM(CASE WHEN Status = 0 THEN 1 ELSE 0 END) AS CompletedTasks
    FROM task
    GROUP BY ProjectID
) AS TaskSummary;
GO
/****** Object:  Table [dbo].[Project]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID]  AS ('PRO'+right('000'+CONVERT([varchar](10),[ID]),(3))) PERSISTED,
	[ProjectName] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](120) NULL,
	[DueDate] [date] NULL,
	[ProjectCreatedDate] [date] NULL,
	[ClientID] [int] NULL,
	[Status] [bit] NULL,
	[FileUpload] [nvarchar](max) NULL,
	[CompletedDate] [date] NULL,
	[CreatedDate] [date] NULL,
	[IsActive] [bit] NULL,
	[ModifiedDate] [date] NULL,
	[CreatedBy] [varchar](30) NULL,
	[ModifiedBy] [varchar](30) NULL,
	[feild1] [varchar](10) NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[TaskTable]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE   VIEW [dbo].[TaskTable]
    AS
        SELECT  T.ID,ED.ID AS EmployeeId, ED.EmployeeCode, T.TaskName, T.DueDate, TS.PercentageCompleted AS Progress, T.Status, P.ID AS ProjectID, P.ProjectName, P.IsActive as ProjectStatus,T.IsActive,ED.ROLE
        FROM Task AS T
            INNER JOIN EmployeeDetails AS ED
            ON T.EmpId =  ED.ID
            INNER JOIN Project AS P
            ON T.ProjectID = P.ID
            INNER JOIN TaskSummary AS TS
            ON TS.ProjectID = P.ID
GO
/****** Object:  Table [dbo].[ReimbursementClaim]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReimbursementClaim](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NOT NULL,
	[Role] [int] NOT NULL,
	[Expense] [int] NOT NULL,
	[BillNo] [varchar](40) NOT NULL,
	[BillDate] [date] NULL,
	[BillAmount] [money] NULL,
	[Bill] [nvarchar](max) NULL,
	[Status] [varchar](30) NULL,
	[CreatedDate] [date] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Reimbursementclaim] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Expensetype]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expensetype](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Expensetypes] [varchar](20) NULL,
 CONSTRAINT [PK_] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ReimbursementAllclaim]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[ReimbursementAllclaim] AS

SELECT ReimbursementClaim.ID,
       EmployeeDetails.FirstName,
       EmployeeDetails.Email,  
       EmployeeDetails.EmployeeCode,
       Designations.Designation,
       Roles.Role,
       Expensetype.Expensetypes,
       ReimbursementClaim.BillNo,
       ReimbursementClaim.BillDate,
       ReimbursementClaim.BillAmount,
       ReimbursementClaim.Bill,
       ReimbursementClaim.Status,
       ReimbursementClaim.CreatedDate,
       ReimbursementClaim.IsActive

FROM   ReimbursementClaim  

INNER JOIN EmployeeDetails ON ReimbursementClaim.EmpId = EmployeeDetails.ID
INNER JOIN Roles ON ReimbursementClaim.Role = Roles.ID
INNER JOIN Expensetype ON ReimbursementClaim.Expense = Expensetype.ID
INNER JOIN Designations ON EmployeeDetails.Designation = Designations.ID
GO
/****** Object:  Table [dbo].[LeaveDetails]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NOT NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[LeaveType] [int] NOT NULL,
	[Reason] [varchar](30) NOT NULL,
	[Detailedreason] [varchar](100) NOT NULL,
	[NoOfDays]  AS (datediff(day,[StartDate],[EndDate])+(1)),
	[Status] [varchar](10) NULL,
	[CreatedDate] [date] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [Pk_leave] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeaveTypeDetails]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveTypeDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Leavetypename] [varchar](20) NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Leaveview]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[Leaveview] as
select  LeaveDetails.ID,
        LeaveDetails.EmpId,
        EmployeeDetails.EmployeeCode,
        EmployeeDetails.FirstName,
        EmployeeDetails.MiddleName,
        EmployeeDetails.LastName,
        Designations.Designation,
        LeaveDetails.StartDate,
        LeaveDetails.EndDate,
        LeaveTypeDetails.Leavetypename,
        LeaveDetails.Reason,
        LeaveDetails.Detailedreason,
        Concat(day (StartDate),' ',FORMAT(StartDate, 'MMMM')) as Wordstartdate,
        Concat(day(EndDate),' ',FORMAT(EndDate,'MMMM')) as WordEndDate,
        LeaveDetails.NoOfDays,
        LeaveDetails.[Status],
        LeaveDetails.IsActive,
        EmployeeDetails.Role
        from ((((EmployeeDetails
        join LeaveDetails on LeaveDetails.EmpId=EmployeeDetails.ID)
        join Designations on Designations.ID=EmployeeDetails.Designation)
        join Roles on EmployeeDetails.Role=Roles.ID)
        join LeaveTypeDetails on LeaveTypeDetails.ID=LeaveDetails.LeaveType);
GO
/****** Object:  View [dbo].[TodayLeaveSample]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[TodayLeaveSample]
AS
SELECT Count(L.EmpId) AS TodayPresent, 'Absent' AS [Status] FROM Leaveview AS L WHERE GETDATE() BETWEEN L.StartDate AND L.EndDate AND [Status]='Approved'
GO
/****** Object:  View [dbo].[montheave]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   view [dbo].[montheave] AS
SELECT EmployeeCode,EmpId,
ISNULL(SUM(CASE
    WHEN month(getdate())=month(StartDate) and month(getdate())=month(EndDate) 
    then  DATEDIFF(day,StartDate,EndDate)

    WHEN month(getdate())=month(StartDate) and month(getdate()) < month(EndDate)
    then DATEDIFF(day,getdate(),StartDate)

    when month(getdate())=month(EndDate) and month(GETDATE()) > month(StartDate)
    then DateDiff(day,DATEFROMPARTS(YEAR(GETDATE()),MONTH(GETDATE()),1),EndDate)

END),0) AS MonthLeavecount
FROM Leaveview
group by EmployeeCode,EmpId;
GO
/****** Object:  Table [dbo].[DisplaySalaryDetails]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DisplaySalaryDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NULL,
	[EmployeeCode] [varchar](20) NULL,
	[BasicSalary] [decimal](11, 2) NULL,
	[HouseRent] [decimal](11, 2) NULL,
	[Conveyance] [decimal](11, 2) NULL,
	[OtherAllowance] [decimal](11, 2) NULL,
	[ESI] [decimal](11, 2) NULL,
	[Tax] [decimal](11, 2) NULL,
	[PF] [decimal](11, 2) NULL,
	[Others] [decimal](11, 2) NULL,
	[Reimbursement] [decimal](11, 2) NULL,
	[NetSalary] [decimal](11, 2) NULL,
	[CreatedAt] [date] NULL,
	[UpdatedAt] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[payslipdetailsview]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create    view [dbo].[payslipdetailsview] AS
SELECT 
    MS.[ID],
    Emp.[ID] As EmpId,
    Emp.[EmployeeCode],
    CONCAT(Emp.[FirstName],'', Emp.[MiddleName],'',Emp.[LastName])As EmployeeName,
    Emp.[Email],
    Emp.[Designation],
    Emp.[DateOfJoining],
    AC.[BankName],
    AC.[AccountNumber],
    AC.[IFSCCode],
    AC.[PFAccount],
    
    MS.[BasicSalary] ,
    MS.[HouseRent] ,
    MS.[Conveyance] ,
    MS.[OtherAllowance] ,
    MS.[ESI] ,
    MS.[Tax] ,
    MS.[PF],
    MS.[Reimbursement],
    MS.[Others],
    MS.[NetSalary],
    MS.[CreatedAt]
from EmployeeBasicDetails as Emp
join  EmployeeAccountDetails As AC
on Ac.EmpId=Emp.ID
join DisplaySalaryDetails As MS
ON MS.EmpId=Emp.ID

GO
/****** Object:  View [dbo].[Applyvalidation]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[Applyvalidation] AS
SELECT ReimbursementClaim.ID,
       ReimbursementAllClaim.EmployeeCode,
       ReimbursementClaim.Role,
       ReimbursementClaim.Expense,
       MONTH(ReimbursementClaim.BillDate) AS [Bill Month]
FROM   ReimbursementClaim
INNER JOIN ReimbursementAllClaim ON ReimbursementClaim.ID = ReimbursementAllClaim.ID
GO
/****** Object:  View [dbo].[ProjectCountDetails]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[ProjectCountDetails]
AS
SELECT COUNT(ProjectID) AS ProjectCount, 
    CASE 
        WHEN [Status] = 0 THEN 'Completed' 
        WHEN [Status] = 1 THEN 'On Progress'
    END AS Status 
FROM Project GROUP BY Status
GO
/****** Object:  Table [dbo].[CompanyPolicy]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyPolicy](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Roles] [int] NOT NULL,
	[LeaveLimit] [int] NOT NULL,
	[InternetLimit] [money] NOT NULL,
	[TravelLimit] [money] NOT NULL,
	[IsActive] [bit] NULL,
	[ModifiedBy] [int] NOT NULL,
	[ModifiedDate] [date] NULL,
 CONSTRAINT [PK_Companypolicy] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[CompanyPolicyMain]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    VIEW [dbo].[CompanyPolicyMain] AS

SELECT Companypolicy.ID,
       Roles.Role,
       Companypolicy.LeaveLimit,
       Companypolicy.InternetLimit,
       Companypolicy.TravelLimit,
       Companypolicy.IsActive,
       EmployeeDetails.EmployeeCode AS ModifiedBy,
       Companypolicy.ModifiedDate

FROM   Companypolicy

INNER JOIN Roles ON  Companypolicy.Roles = Roles.ID
INNER JOIN EmployeeDetails ON Companypolicy.ModifiedBy = EmployeeDetails.ID
GO
/****** Object:  View [dbo].[Leavepolicy]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Leavepolicy] AS
SELECT ID,
       [Role],
       LeaveLimit
FROM  CompanyPolicyMain 
GO
/****** Object:  View [dbo].[Expensepolicy]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Expensepolicy] AS
SELECT ID,
       [Role],
       InternetLimit,
       TravelLimit
FROM  CompanyPolicyMain  
GO
/****** Object:  View [dbo].[Leavepolicyview]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Leavepolicyview] AS
SELECT ID,
       [Role],
       LeaveLimit
FROM  CompanyPolicyMain 
GO
/****** Object:  View [dbo].[ReimbursementRequests]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[ReimbursementRequests] AS

SELECT ReimbursementAllclaim.ID,
       ReimbursementAllclaim.EmployeeCode,
       ReimbursementAllclaim.FirstName,
       ReimbursementAllclaim.Role,
       ReimbursementAllclaim.Designation,
       ReimbursementAllclaim.Expensetypes,
       ReimbursementAllclaim.BillNo,
       ReimbursementAllclaim.BillDate,
       ReimbursementAllclaim.CreatedDate,
       ReimbursementAllclaim.BillAmount,
       ReimbursementAllclaim.Status,
       ReimbursementAllclaim.Bill

FROM ReimbursementAllclaim
GO
/****** Object:  View [dbo].[Expensepolicyview]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Expensepolicyview] AS
SELECT ID,
       [Role],
       InternetLimit,
       TravelLimit
FROM  CompanyPolicyMain 
GO
/****** Object:  View [dbo].[EmployeeLoginDetails]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   view [dbo].[EmployeeLoginDetails]
As
select 
        Emp.[EmployeeCode],
        CONCAT(Emp.[FirstName],' ',COALESCE(Emp.[MiddleName],' '),' ',Emp.[LastName]) AS EmployeeName,
        Emp.[Email],
        Emp.[Designation],
        -- Emp.[DateOfJoining],
        Ds.[BasicSalary],
        Ds.[CreatedAt]
        from EmployeeBasicDetails As Emp
        JOIN DisplaySalaryDetails As Ds
        ON Emp.EmployeeCode=Ds.EmployeeCode
GO
/****** Object:  View [dbo].[monthleave]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   view [dbo].[monthleave] AS
SELECT EmployeeCode,EmpId,
ISNULL(SUM(CASE
    WHEN month(getdate())=month(StartDate) and month(getdate())=month(EndDate)
    then  DATEDIFF(day,StartDate,EndDate)+1

    WHEN month(getdate())=month(StartDate) and month(getdate()) < month(EndDate)
    then DATEDIFF(day,getdate(),StartDate)+1

    when month(getdate())=month(EndDate) and month(GETDATE()) > month(StartDate)
    then DateDiff(day,DATEFROMPARTS(YEAR(GETDATE()),MONTH(GETDATE()),1),EndDate)+1

END),0) AS MonthLeavecount
FROM Leaveview WHERE Status='Approved'
group by EmployeeCode,EmpId;
GO
/****** Object:  View [dbo].[MonthlySalaryCalc]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   view [dbo].[MonthlySalaryCalc] AS
Select 
    E.ID,
    E.EmployeeCode,
    CONCAT(E.[FirstName],' ',COALESCE(E.[MiddleName],' '),' ',E.[LastName]) AS EmployeeName,
    E.Role,
    E.Experience,
    E.DateOfJoining,
    Ac.Salary As BasicSalary,
        sum(CASE 
        WHEN MONTH(GETDATE())=month(R.BillDate)
        then R.BillAmount
        ELSE
        0
        END )As Reimbursement,
    ISNULL(L.MonthLeavecount, 0) AS Leave
     from EmployeeBasicDetails AS E
     join EmployeeAccountDetails As AC
     on E.Id=Ac.EmpId
     left join MonthLeave As L
     on E.ID=L.EmpId
     LEFT JOIN reimbursementclaim As R
     on E.ID=R.EmpId
     group  by E.Id,E.employeecode,E.EmployeeCode,E.[FirstName],E.[MiddleName],E.[LastName], E.Role,E.Experience,E.DateOfJoining,Ac.Salary ,L.MonthLeavecount
GO
/****** Object:  Table [dbo].[EmployeeLogin]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeLogin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NULL,
	[UserName] [nvarchar](30) NULL,
	[Password] [nvarchar](30) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_LoginId] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[LoginDetails]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[LoginDetails]
AS
SELECT ED.[ID], ED.[FirstName], EL.Password, EL.UserName, R.[Role], ED.[Role] AS RoleId, ProfileImage, CONCAT(ED.[FirstName],' ',COALESCE(ED.[MiddleName],' '),' ',ED.[LastName]) AS FullName  FROM EmployeeLogin AS EL 
INNER JOIN EmployeeDetails AS ED ON EL.EmpId=ED.ID
INNER JOIN Roles AS R ON ED.Role=R.ID 
WHERE ED.IsActive=1
GO
/****** Object:  Table [dbo].[ResignationTable]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResignationTable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NULL,
	[LastDateOfWorking] [date] NOT NULL,
	[ReasonForResign] [varchar](max) NOT NULL,
	[AdditionalReasonForResign] [varchar](max) NULL,
	[Status] [varchar](20) NULL,
	[IsActive] [bit] NULL,
	[Field2] [varchar](30) NULL,
	[Field3] [varchar](30) NULL,
 CONSTRAINT [PK_ResignationTable] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[Resignationview]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[Resignationview] AS
SELECT EmployeeBasicDetails.[ID],
EmployeeBasicDetails.[EmployeeCode],
EmployeeBasicDetails.[FirstName],
EmployeeBasicDetails.[DateOfJoining],
EmployeeBasicDetails.[Designation],
ResignationTable.[LastDateOfWorking],
ResignationTable.[ReasonForResign],
ResignationTable.[AdditionalReasonForResign],
ResignationTable.[Status],
EmployeeBasicDetails.[IsActive]

FROM EmployeeBasicDetails
INNER JOIN ResignationTable ON ResignationTable.[EmpId]=EmployeeBasicDetails.[ID]
WHERE ResignationTable.[Status]='Pending';
GO
/****** Object:  View [dbo].[TotalEmpLeave]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[TotalEmpLeave]
AS
SELECT E.[ID], E.[FirstName], ISNULL(SUM(L.NoOfDays), 0) AS TotalLeave FROM EmployeeDetails AS E
LEFT JOIN LeaveDetails AS L ON L.EmpId = E.ID AND Month(L.StartDate) = Month(GETDATE())
GROUP BY E.[FirstName], E.[ID]
HAVING ISNULL(SUM(L.NoOfDays), 0) <= 5
GO
/****** Object:  View [dbo].[TotalEmpTaskComplete]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[TotalEmpTaskComplete]
AS
SELECT E.[FirstName],D.[Designation], COUNT(T.Status) AS TaskComplete
FROM EmployeeDetails AS E
INNER JOIN Task AS T ON T.EmpId = E.ID 
INNER JOIN Designations AS D ON E.Designation = D.ID
WHERE T.Status = 0 AND Month(T.CompletedAt)=Month(GETDATE())
GROUP BY E.[FirstName],T.Status, D.Designation
GO
/****** Object:  View [dbo].[EmpOfMonth]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Final Employee Of Month
CREATE   VIEW [dbo].[EmpOfMonth]
AS
SELECT TOP 1 TL.FirstName, TT.Designation FROM TotalEmpLeave AS TL
INNER JOIN TotalEmpTaskComplete AS TT ON TL.FirstName=TT.FirstName
ORDER BY TL.TotalLeave ASC, TT.TaskComplete DESC
GO
/****** Object:  Table [dbo].[Client]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ClientCompany] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectMembers]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectMembers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TeamMembersID] [int] NULL,
	[ProjectID] [int] NULL,
	[Role] [int] NULL,
	[Module] [varchar](15) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_ProjectMembers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ProjectTable]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    CREATE   VIEW [dbo].[ProjectTable]
    AS
        SELECT P.ID, P.ProjectID, P.[ProjectName], P.[Description], P.[DueDate], (TS.TotalTasks - TS.CompletedTasks) AS OpenTask, TS.CompletedTasks, TS.TotalTasks AS NoOfTask, TS.PercentageCompleted AS Progress ,P.Status  ,P.CreatedDate, P.ClientID, C.ClientCompany, P.IsActive
        FROM Project AS P
            INNER JOIN ProjectMembers AS PM
            ON P.ID = PM.ProjectID
            INNER JOIN Client AS C
            ON P.ClientID = C.ID
            INNER JOIN TaskSummary AS TS
            ON P.ID = TS.ProjectID
        GROUP BY PM.ProjectID,P.[ProjectName],P.[Description],P.[DueDate],P.ClientID,C.ClientCompany,P.ID,P.Status,P.ProjectID,P.IsActive,P.CreatedDate,TS.TotalTasks,TS.CompletedTasks,TS.PercentageCompleted
GO
/****** Object:  View [dbo].[EmpLeaveDetails]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[EmpLeaveDetails]
AS
SELECT E.[ID], E.[EmployeeCode], E.[FirstName], R.[Role], ISNULL(SUM(L.[NoOfDays]), 0) AS TakenLeave, C.[LeaveLimit] FROM EmployeeDetails AS E 
INNER JOIN CompanyPolicy AS C ON E.[Role] = C.[Roles]
INNER JOIN Roles AS R ON R.[ID] = E.[Role]
LEFT JOIN LeaveDetails AS L ON E.[ID] = L.[EmpId] AND L.[Status] = 'Approved'
GROUP BY  E.[ID], E.[EmployeeCode], E.[FirstName], R.[Role], C.[LeaveLimit]
--ORDER BY L.[EmpId] ASC
GO
/****** Object:  View [dbo].[LeaveRemaining]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[LeaveRemaining]
AS
SELECT ID, 'Taken Leave' AS [LeaveDetails], TakenLeave, 0 AS LeaveLimit FROM EmpLeaveDetails
UNION ALL
SELECT ID, 'Remaining Leave' AS [LeaveDetails], 0 AS TakenLeave, LeaveLimit-TakenLeave AS LeaveLimit FROM EmpLeaveDetails
GO
/****** Object:  View [dbo].[TeamMember]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    create   VIEW [dbo].[TeamMember]
    AS
        SELECT TM.ID,TM.TeamMembersID, ED.FirstName +' '+ED.MiddleName +' '+ED.LastName AS FullName , TM.ProjectID, P.ProjectName ,TM.Role AS ROLEID, R.Role, TM.Module , TM.IsActive
        from ProjectMembers AS TM
            INNER JOIN Roles As R
            ON TM.Role = R.ID
            INNER JOIN EmployeeDetails AS ED
            ON TM.TeamMembersID = ED.ID
            INNER JOIN Project AS P
            ON TM.ProjectID = P.ID
GO
/****** Object:  View [dbo].[leavetypeview]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   view [dbo].[leavetypeview] as
SELECT E.ID,
       E.EmployeeCode,
       E.Role,
       SUM(L.NoOfDays) AS Leave,
       LT.Leavetypename,
       LT.ID as leavetypeid,
       cp.LeaveLimit,
       cp.LeaveLimit-(SUM(L.NoOfDays)) as Remainingleave,
        L.[Status]
FROM (((EmployeeDetails AS E
INNER JOIN LeaveDetails AS L ON L.EmpId = E.ID)
INNER JOIN LeaveTypeDetails AS LT ON LT.ID = L.LeaveType)
INNER JOIN CompanyPolicy as cp on E.Role=cp.Roles)
GROUP BY LT.Leavetypename, L.NoOfDays ,E.EmployeeCode,LT.ID,cp.LeaveLimit,E.Role,E.ID,L.Status
GO
/****** Object:  Table [dbo].[Attendance]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendance](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[PunchIn] [nvarchar](30) NOT NULL,
	[PunchOut] [nvarchar](30) NULL,
	[WorkingHours] [nvarchar](40) NULL,
	[CreatedDate] [date] NOT NULL,
	[ModifiedDate] [date] NULL,
	[CreatedBy] [varchar](1) NULL,
	[ModifiedBy] [varchar](1) NULL,
	[ExtraField] [varchar](1) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Attendance] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[DailyAttendance]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[DailyAttendance] AS
SELECT Attendance.[Id], Attendance.[EmpId], EmployeeDetails.[EmployeeCode], CONCAT(EmployeeDetails.[FirstName],' ', EmployeeDetails.[MiddleName],' ', EmployeeDetails.[LastName]) AS EmployeeName, Attendance.[Date], Attendance.[PunchIn], Attendance.[PunchOut], Attendance.[WorkingHours], Attendance.[IsActive]
FROM Attendance
INNER JOIN EmployeeDetails ON Attendance.EmpId = EmployeeDetails.ID;
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ticket](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NOT NULL,
	[TicketId]  AS ('TCK'+right('0000'+CONVERT([varchar](10),[id]),(4))) PERSISTED,
	[TicketSubject] [int] NULL,
	[Priority] [varchar](15) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Status] [varchar](10) NULL,
	[RaiseDate] [date] NOT NULL,
	[AttachFile] [nvarchar](max) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[ModifiedDate] [date] NULL,
	[CreatedBy] [int] NULL,
	[IsActive] [bit] NULL,
	[Field1] [varchar](10) NULL,
	[Field2] [varchar](10) NULL,
	[Field3] [varchar](10) NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TicketSubject]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketSubject](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Subject] [varchar](30) NULL,
	[PriorityId] [int] NULL,
 CONSTRAINT [PK_TicketSubject] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Priority]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Priority](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Priority] [varchar](10) NULL,
 CONSTRAINT [PK_Priority] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[TicketRaised]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[TicketRaised] AS 
SELECT T.[ID], T.[TicketId],T.[EmpId],TSUB.[Subject],E.[FirstName],E.[MiddleName],D.[Designation],T.[RaiseDate],P.[Priority],T.[Status],T.[Description],T.[AttachFile],T.[CreatedDate], T.[IsActive],T.[CreatedBy] FROM EmployeeDetails AS E 
INNER JOIN Ticket AS T ON E.ID=T.EmpId
INNER JOIN TicketSubject AS TSUB ON TSUB.ID = T.TicketSubject
INNER JOIN Priority AS P ON P.ID = T.Priority 
INNER JOIN Designations AS D ON E.Designation = D.ID
GO
/****** Object:  View [dbo].[sickchartview]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   view [dbo].[sickchartview] as
WITH LeaveData AS (
    SELECT 
        ID, 
        EmployeeCode, 
        Leavetypename, 
        LeaveLimit, 
        SUM(leave) AS leavetypecount,
        [Status]
    FROM 
        leavetypeview
    WHERE 
        EmployeeCode = 'BYT002' AND Leavetypename = 'Sick Leave'
    GROUP BY 
        ID, EmployeeCode, Leavetypename, LeaveLimit,[Status]
)
SELECT 
    COALESCE(ld.ID, dd.ID) AS ID, 
    'BYT002' AS EmployeeCode, 
    COALESCE(ld.leavetypecount, 0) AS leavetypecount, 
    'Sick Leave' AS Leavetypename, 
    COALESCE(ld.LeaveLimit, dd.DefaultLeaveLimit) AS LeaveLimit,
    COALESCE(ld.LeaveLimit, dd.DefaultLeaveLimit) - COALESCE(ld.leavetypecount, 0) AS Remainingleave
FROM 
    (SELECT 
        ID, 
        LeaveLimit AS DefaultLeaveLimit, 
        EmployeeCode
     FROM 
        leavetypeview 
     WHERE 
        EmployeeCode = 'BYT002'
     GROUP BY 
        ID, LeaveLimit, EmployeeCode) dd
LEFT JOIN 
    LeaveData ld
    ON dd.ID = ld.ID AND dd.EmployeeCode = ld.EmployeeCode;
GO
/****** Object:  View [dbo].[TotalEntry]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   VIEW [dbo].[TotalEntry] AS
SELECT [EmployeeCode] AS AttendanceCount FROM DailyAttendance
WHERE [Date] = convert(Date, getdate()) 
GROUP BY  DailyAttendance.[EmployeeCode]
GO
/****** Object:  View [dbo].[DayCount]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[DayCount] AS
WITH LeaveCount AS (
    SELECT COUNT(EmployeeCode) AS TotalTodayLeave
    FROM Leaveview
    WHERE [Status] = 'Approved' AND GETDATE() BETWEEN StartDate AND EndDate
),
EntryCount AS (
    SELECT COUNT(*) AS TotalEntry
    FROM TotalEntry
)
SELECT  
    (SELECT TotalTodayLeave FROM LeaveCount) AS [Absent],
    (SELECT TotalEntry FROM EntryCount) AS [Present];
GO
/****** Object:  View [dbo].[MonthlyReimbursementTotals]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    VIEW [dbo].[MonthlyReimbursementTotals] AS

SELECT EmployeeDetails.ID,
       EmployeeDetails.EmployeeCode,
       MONTH(ReimbursementClaim.BillDate) AS Month,
       YEAR(ReimbursementClaim.BillDate) AS Year,
       SUM(ReimbursementClaim.BillAmount) AS TotalClaimAmount

FROM   ReimbursementClaim  

INNER JOIN EmployeeDetails ON ReimbursementClaim.EmpId = EmployeeDetails.ID

GROUP BY EmployeeDetails.ID, EmployeeDetails.EmployeeCode, MONTH(ReimbursementClaim.BillDate), YEAR(ReimbursementClaim.BillDate)
GO
/****** Object:  View [dbo].[ViewProfile]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[ViewProfile] AS
SELECT E.[FirstName],E.[MiddleName],E.[LastName],R.[Role],D.[Designation],E.[EmployeeCode],E.[DateOfJoining],E.[MobileNumber],E.[Email],E.[Address],E.[City],E.[Nationality],E.[Religion],E.[MaritalStatus],A.[BankName],A.[AccountNumber],A.[IFSCCode],Edu.[PgCollegeName],Edu.[PgStart],Edu.[PgEnd],Edu.[PgCourse],Edu.[UgCollageName],Edu.[UgStart],Edu.[UgEnd],Edu.[UgCourse],Edu.[HSCSchoolName],Edu.[HSCStart],Edu.[HSCEnd],Edu.[HSCCourse],Edu.[SSLCSchoolName],Edu.[SSLCStart],Edu.[SSLCEnd],Edu.[EmergencyContact],Edu.[Relationship] FROM EmployeeDetails AS E 
INNER JOIN Designations AS D ON E.Designation = D.ID 
INNER JOIN Roles AS R ON E.Role = R.ID
INNER JOIN EmployeeAccountDetails AS A ON E.ID = A.EmpId
INNER JOIN EmployeePersonalDetails AS Edu ON E.ID = Edu.EmpId
GO
/****** Object:  View [dbo].[EmployeeSalaryDetails]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   view [dbo].[EmployeeSalaryDetails] As
select Emp.[ID],Emp.[EmployeeCode],CONCAT(Emp.[FirstName],' ',COALESCE(Emp.[MiddleName],' '),' ',Emp.[LastName]) AS FullName,[Email],Desig.[Designation],SD.[BasicPay] As [Salary]
from EmployeeDetails As Emp
INNER JOIN Designations As Desig
ON Desig.ID=Emp.Designation
inner join SalaryDetails AS SD
on Emp.Id=SD.EmpId
GO
/****** Object:  View [dbo].[SHOWSALARYDETAILS]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[SHOWSALARYDETAILS] AS
SELECT 
    [ID] ,
    [EmployeeName],
    [AdjustedBasicPay] As BasicPay,
    [HouseRent],
    [Conveyance] ,
    [OtherAllowance] ,
    [ESI] ,
    [Tax] ,
    [PF] ,
    [Others]  ,
    [CreatedDate] 
    FROM MonthlySalary 
GO
/****** Object:  Table [dbo].[Holidays]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Holidays](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HolidayName] [varchar](20) NOT NULL,
	[Date] [date] NOT NULL,
	[Day]  AS (datename(weekday,[Date])),
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Holiday] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reasons]    Script Date: 11-06-2024 11:56:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reasons](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[problem] [varchar](80) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeSchedule]    Script Date: 11-06-2024 11:57:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeSchedule](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EventName] [varchar](20) NOT NULL,
	[Time] [nvarchar](20) NULL,
	[TimeOut] [nvarchar](20) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_TimeSchedulePK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Attendance] ON 

INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (1, 1, CAST(N'2024-06-08' AS Date), N'15:37:48', N'15:49:08', N'0 Hours 12 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (2, 2, CAST(N'2024-06-08' AS Date), N'15:42:11', N'16:43:23', N'1 Hours 1 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (3, 2, CAST(N'2024-06-08' AS Date), N'15:42:46', N'16:43:23', N'1 Hours 1 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (4, 1, CAST(N'2024-06-08' AS Date), N'15:45:05', N'15:49:08', N'0 Hours 4 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (5, 1, CAST(N'2024-06-08' AS Date), N'15:45:17', N'15:49:08', N'0 Hours 4 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (6, 1, CAST(N'2024-06-08' AS Date), N'15:46:41', N'15:49:08', N'0 Hours 3 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (7, 1, CAST(N'2024-06-08' AS Date), N'15:46:59', N'15:49:08', N'0 Hours 3 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (8, 1, CAST(N'2024-06-08' AS Date), N'15:49:10', N'16:05:14', N'0 Hours 16 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (9, 2, CAST(N'2024-06-08' AS Date), N'15:55:24', N'16:43:23', N'0 Hours 48 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (10, 1, CAST(N'2024-06-08' AS Date), N'15:55:51', N'16:05:14', N'0 Hours 10 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (11, 2, CAST(N'2024-06-08' AS Date), N'15:56:25', N'16:43:23', N'0 Hours 47 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (12, 1, CAST(N'2024-06-08' AS Date), N'16:00:42', N'16:05:14', N'0 Hours 5 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (13, 1, CAST(N'2024-06-08' AS Date), N'16:02:57', N'16:05:14', N'0 Hours 3 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (14, 1, CAST(N'2024-06-08' AS Date), N'16:05:04', N'16:05:14', N'0 Hours 0 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (15, 8, CAST(N'2024-06-08' AS Date), N'16:05:34', N'10:58:10', N'-5 Hours -7 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (16, 1, CAST(N'2024-06-08' AS Date), N'16:12:26', N'16:43:04', N'0 Hours 31 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (17, 1, CAST(N'2024-06-08' AS Date), N'16:14:31', N'16:43:04', N'0 Hours 29 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (18, 1, CAST(N'2024-06-08' AS Date), N'16:20:20', N'16:43:04', N'0 Hours 23 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (19, 1, CAST(N'2024-06-08' AS Date), N'16:20:20', N'16:43:04', N'0 Hours 23 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (20, 1, CAST(N'2024-06-08' AS Date), N'16:20:20', N'16:43:04', N'0 Hours 23 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (21, 1, CAST(N'2024-06-08' AS Date), N'16:20:20', N'16:43:04', N'0 Hours 23 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (22, 1, CAST(N'2024-06-08' AS Date), N'16:20:20', N'16:43:04', N'0 Hours 23 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (23, 1, CAST(N'2024-06-08' AS Date), N'16:20:57', N'16:43:04', N'0 Hours 23 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (24, 1, CAST(N'2024-06-08' AS Date), N'16:33:01', N'16:43:04', N'0 Hours 10 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (25, 1, CAST(N'2024-06-08' AS Date), N'16:33:01', N'16:43:04', N'0 Hours 10 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (26, 1, CAST(N'2024-06-08' AS Date), N'16:36:03', N'16:43:04', N'0 Hours 7 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (27, 1, CAST(N'2024-06-08' AS Date), N'16:36:03', N'16:43:04', N'0 Hours 7 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (28, 1, CAST(N'2024-06-08' AS Date), N'16:38:08', N'16:43:04', N'0 Hours 5 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (29, 1, CAST(N'2024-06-08' AS Date), N'16:41:55', N'16:43:04', N'0 Hours 2 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (30, 1, CAST(N'2024-06-08' AS Date), N'16:42:30', N'16:43:04', N'0 Hours 1 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (31, 2, CAST(N'2024-06-08' AS Date), N'16:43:13', N'16:43:23', N'0 Hours 0 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (32, 1, CAST(N'2024-06-08' AS Date), N'16:43:26', N'16:43:49', N'0 Hours 0 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (33, 1, CAST(N'2024-06-08' AS Date), N'16:43:26', N'16:43:49', N'0 Hours 0 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (34, 1, CAST(N'2024-06-08' AS Date), N'16:43:50', N'16:49:56', N'0 Hours 6 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (35, 1, CAST(N'2024-06-08' AS Date), N'16:48:22', N'16:49:56', N'0 Hours 1 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (36, 1, CAST(N'2024-06-08' AS Date), N'16:49:07', N'16:49:56', N'0 Hours 0 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (37, 1, CAST(N'2024-06-08' AS Date), N'16:50:22', N'16:50:35', N'0 Hours 0 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (38, 2, CAST(N'2024-06-08' AS Date), N'16:50:37', N'16:50:47', N'0 Hours 0 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (39, 1, CAST(N'2024-06-08' AS Date), N'16:50:50', N'10:07:40', N'-6 Hours -43 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (40, 1, CAST(N'2024-06-08' AS Date), N'16:58:53', N'10:07:40', N'-6 Hours -51 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (41, 1, CAST(N'2024-06-08' AS Date), N'17:05:11', N'10:07:40', N'-6 Hours -58 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (42, 1, CAST(N'2024-06-08' AS Date), N'17:28:43', N'10:07:40', N'-7 Hours -21 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (43, 1, CAST(N'2024-06-08' AS Date), N'17:28:44', N'10:07:40', N'-7 Hours -21 Minutes ', CAST(N'2024-06-08' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (44, 1, CAST(N'2024-06-10' AS Date), N'10:05:02', N'10:07:40', N'0 Hours 2 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (45, 2, CAST(N'2024-06-10' AS Date), N'10:07:54', N'10:08:10', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (46, 8, CAST(N'2024-06-10' AS Date), N'10:08:20', N'10:58:10', N'0 Hours 50 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (47, 1, CAST(N'2024-06-10' AS Date), N'10:12:44', N'10:25:04', N'0 Hours 13 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (48, 3, CAST(N'2024-06-10' AS Date), N'10:24:52', N'10:34:08', N'0 Hours 10 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (49, 3, CAST(N'2024-06-10' AS Date), N'10:25:40', N'10:34:08', N'0 Hours 9 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (50, 1, CAST(N'2024-06-10' AS Date), N'10:34:13', N'11:06:20', N'0 Hours 32 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (51, 8, CAST(N'2024-06-10' AS Date), N'10:58:12', N'Still Logined', N'...', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (52, 1, CAST(N'2024-06-10' AS Date), N'11:07:07', N'11:07:11', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (53, 3, CAST(N'2024-06-10' AS Date), N'11:07:20', N'12:02:20', N'0 Hours 55 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (54, 3, CAST(N'2024-06-10' AS Date), N'11:10:05', N'12:02:20', N'0 Hours 52 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (55, 3, CAST(N'2024-06-10' AS Date), N'11:50:29', N'12:02:20', N'0 Hours 12 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (56, 3, CAST(N'2024-06-10' AS Date), N'11:51:15', N'12:02:20', N'0 Hours 11 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (57, 1, CAST(N'2024-06-10' AS Date), N'12:01:49', N'12:01:57', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (58, 2, CAST(N'2024-06-10' AS Date), N'12:01:59', N'12:02:02', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (59, 3, CAST(N'2024-06-10' AS Date), N'12:02:06', N'12:02:20', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (60, 4, CAST(N'2024-06-10' AS Date), N'12:02:50', N'12:03:09', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (61, 5, CAST(N'2024-06-10' AS Date), N'12:03:37', N'12:03:45', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (62, 5, CAST(N'2024-06-10' AS Date), N'12:03:48', N'12:12:25', N'0 Hours 9 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (63, 1, CAST(N'2024-06-10' AS Date), N'12:12:29', N'12:36:08', N'0 Hours 24 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (64, 1, CAST(N'2024-06-10' AS Date), N'12:36:02', N'12:36:08', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (65, 1, CAST(N'2024-06-10' AS Date), N'12:36:15', N'12:39:44', N'0 Hours 3 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (66, 7, CAST(N'2024-06-10' AS Date), N'12:39:52', N'12:49:15', N'0 Hours 10 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (67, 1, CAST(N'2024-06-10' AS Date), N'12:41:19', N'12:44:27', N'0 Hours 3 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (68, 7, CAST(N'2024-06-10' AS Date), N'12:44:30', N'18:46:20', N'6 Hours 2 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (69, 6, CAST(N'2024-06-10' AS Date), N'12:46:29', N'12:52:41', N'0 Hours 6 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (70, 1, CAST(N'2024-06-10' AS Date), N'12:49:31', N'12:49:38', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (71, 2, CAST(N'2024-06-10' AS Date), N'12:49:42', N'12:50:56', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (72, 6, CAST(N'2024-06-10' AS Date), N'12:51:02', N'12:52:41', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (73, 1, CAST(N'2024-06-10' AS Date), N'12:52:45', N'12:53:54', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (74, 6, CAST(N'2024-06-10' AS Date), N'12:53:58', N'12:54:09', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (75, 3, CAST(N'2024-06-10' AS Date), N'12:54:13', N'12:54:18', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (76, 4, CAST(N'2024-06-10' AS Date), N'12:54:22', N'12:56:55', N'0 Hours 2 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (77, 1, CAST(N'2024-06-10' AS Date), N'12:56:57', N'13:15:49', N'0 Hours 19 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (78, 6, CAST(N'2024-06-10' AS Date), N'13:15:53', N'13:18:17', N'0 Hours 3 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (79, 13, CAST(N'2024-06-10' AS Date), N'13:18:39', N'14:05:45', N'0 Hours 47 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (80, 6, CAST(N'2024-06-10' AS Date), N'14:05:43', N'14:06:17', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (81, 1, CAST(N'2024-06-10' AS Date), N'14:05:49', N'14:09:30', N'0 Hours 4 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (82, 1, CAST(N'2024-06-10' AS Date), N'14:06:20', N'14:09:30', N'0 Hours 3 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (83, 2, CAST(N'2024-06-10' AS Date), N'14:09:40', N'14:10:37', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (84, 2, CAST(N'2024-06-10' AS Date), N'14:10:10', N'14:10:37', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (85, 1, CAST(N'2024-06-10' AS Date), N'14:10:39', N'14:10:57', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (86, 6, CAST(N'2024-06-10' AS Date), N'14:11:00', N'15:06:56', N'0 Hours 55 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (87, 1, CAST(N'2024-06-10' AS Date), N'14:21:25', N'15:13:51', N'0 Hours 52 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (88, 1, CAST(N'2024-06-10' AS Date), N'14:22:02', N'15:13:51', N'0 Hours 51 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (89, 1, CAST(N'2024-06-10' AS Date), N'15:06:58', N'15:13:51', N'0 Hours 7 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (90, 14, CAST(N'2024-06-10' AS Date), N'15:15:27', N'15:18:37', N'0 Hours 3 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (91, 14, CAST(N'2024-06-10' AS Date), N'15:16:52', N'15:18:37', N'0 Hours 2 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (92, 1, CAST(N'2024-06-10' AS Date), N'15:19:19', N'15:20:24', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (93, 1, CAST(N'2024-06-10' AS Date), N'15:22:56', N'15:23:26', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (94, 6, CAST(N'2024-06-10' AS Date), N'15:23:31', N'15:24:03', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (95, 2, CAST(N'2024-06-10' AS Date), N'15:24:07', N'15:24:15', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (96, 6, CAST(N'2024-06-10' AS Date), N'15:24:32', N'15:39:03', N'0 Hours 15 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (97, 1, CAST(N'2024-06-10' AS Date), N'15:37:50', N'15:38:34', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (98, 6, CAST(N'2024-06-10' AS Date), N'15:38:37', N'15:39:03', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (99, 2, CAST(N'2024-06-10' AS Date), N'15:39:16', N'15:40:58', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (100, 1, CAST(N'2024-06-10' AS Date), N'15:41:02', N'15:41:20', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (101, 6, CAST(N'2024-06-10' AS Date), N'15:41:23', N'15:41:54', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (102, 1, CAST(N'2024-06-10' AS Date), N'15:41:57', N'15:43:27', N'0 Hours 2 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (103, 6, CAST(N'2024-06-10' AS Date), N'15:43:30', N'15:44:42', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (104, 1, CAST(N'2024-06-10' AS Date), N'15:44:46', N'15:55:37', N'0 Hours 11 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (105, 1, CAST(N'2024-06-10' AS Date), N'15:45:12', N'15:55:37', N'0 Hours 10 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (106, 4, CAST(N'2024-06-10' AS Date), N'15:55:40', N'15:56:38', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (107, 4, CAST(N'2024-06-10' AS Date), N'15:56:40', N'15:56:41', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (108, 1, CAST(N'2024-06-10' AS Date), N'15:56:45', N'15:58:36', N'0 Hours 2 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (109, 4, CAST(N'2024-06-10' AS Date), N'15:58:39', N'16:02:16', N'0 Hours 4 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (110, 1, CAST(N'2024-06-10' AS Date), N'16:02:19', N'16:23:24', N'0 Hours 21 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (111, 1, CAST(N'2024-06-10' AS Date), N'16:04:01', N'16:23:24', N'0 Hours 19 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (112, 1, CAST(N'2024-06-10' AS Date), N'16:13:39', N'16:23:24', N'0 Hours 10 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (113, 1, CAST(N'2024-06-10' AS Date), N'16:22:20', N'16:23:24', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (114, 2, CAST(N'2024-06-10' AS Date), N'16:23:27', N'16:24:48', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (115, 2, CAST(N'2024-06-10' AS Date), N'16:29:20', N'16:31:05', N'0 Hours 2 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (116, 1, CAST(N'2024-06-10' AS Date), N'16:30:55', N'16:33:43', N'0 Hours 3 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (117, 1, CAST(N'2024-06-10' AS Date), N'16:31:07', N'16:33:43', N'0 Hours 2 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (118, 7, CAST(N'2024-06-10' AS Date), N'16:34:01', N'16:36:56', N'0 Hours 2 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (119, 2, CAST(N'2024-06-10' AS Date), N'16:35:10', N'16:35:17', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (120, 6, CAST(N'2024-06-10' AS Date), N'16:35:21', N'16:35:32', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (121, 1, CAST(N'2024-06-10' AS Date), N'16:35:35', N'16:36:35', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (122, 7, CAST(N'2024-06-10' AS Date), N'16:36:39', N'16:36:56', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (123, 2, CAST(N'2024-06-10' AS Date), N'16:37:07', N'16:37:10', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (124, 7, CAST(N'2024-06-10' AS Date), N'16:37:14', N'16:38:04', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (125, 7, CAST(N'2024-06-10' AS Date), N'16:38:09', N'16:38:10', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (126, 1, CAST(N'2024-06-10' AS Date), N'16:38:17', N'16:39:21', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (127, 1, CAST(N'2024-06-10' AS Date), N'16:38:23', N'16:39:21', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (128, 7, CAST(N'2024-06-10' AS Date), N'16:39:24', N'16:39:52', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (129, 1, CAST(N'2024-06-10' AS Date), N'16:39:55', N'16:40:01', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (130, 2, CAST(N'2024-06-10' AS Date), N'16:40:04', N'16:40:18', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (131, 7, CAST(N'2024-06-10' AS Date), N'16:40:21', N'16:40:23', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (132, 7, CAST(N'2024-06-10' AS Date), N'16:40:37', N'16:40:42', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (133, 3, CAST(N'2024-06-10' AS Date), N'16:40:42', N'16:40:53', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (134, 1, CAST(N'2024-06-10' AS Date), N'16:40:44', N'16:47:00', N'0 Hours 7 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (135, 2, CAST(N'2024-06-10' AS Date), N'16:40:56', N'16:41:18', N'0 Hours 1 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (136, 4, CAST(N'2024-06-10' AS Date), N'16:41:21', N'16:41:49', N'0 Hours 0 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (137, 1, CAST(N'2024-06-10' AS Date), N'16:41:51', N'16:47:00', N'0 Hours 6 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (138, 1, CAST(N'2024-06-10' AS Date), N'16:44:56', N'16:47:00', N'0 Hours 3 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (139, 3, CAST(N'2024-06-10' AS Date), N'16:47:04', N'16:51:20', N'0 Hours 4 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (140, 1, CAST(N'2024-06-10' AS Date), N'16:51:24', N'16:53:36', N'0 Hours 2 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (141, 3, CAST(N'2024-06-10' AS Date), N'16:53:42', N'16:59:28', N'0 Hours 6 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (142, 13, CAST(N'2024-06-10' AS Date), N'16:59:45', N'17:22:32', N'0 Hours 23 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (143, 1, CAST(N'2024-06-10' AS Date), N'17:02:51', N'17:25:35', N'0 Hours 23 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (144, 1, CAST(N'2024-06-10' AS Date), N'17:04:59', N'17:25:35', N'0 Hours 21 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (145, 1, CAST(N'2024-06-10' AS Date), N'17:10:06', N'17:25:35', N'0 Hours 15 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (146, 1, CAST(N'2024-06-10' AS Date), N'17:10:31', N'17:25:35', N'0 Hours 15 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (147, 1, CAST(N'2024-06-10' AS Date), N'17:22:37', N'17:25:35', N'0 Hours 3 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (148, 7, CAST(N'2024-06-10' AS Date), N'17:25:39', N'17:29:53', N'0 Hours 4 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (149, 1, CAST(N'2024-06-10' AS Date), N'17:29:57', N'10:46:48', N'-6 Hours -43 Minutes ', CAST(N'2024-06-10' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (150, 3, CAST(N'2024-06-11' AS Date), N'09:46:53', N'11:07:09', N'1 Hours 21 Minutes ', CAST(N'2024-06-11' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (151, 1, CAST(N'2024-06-11' AS Date), N'09:50:15', N'10:46:48', N'0 Hours 56 Minutes ', CAST(N'2024-06-11' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (152, 2, CAST(N'2024-06-11' AS Date), N'10:46:53', N'11:05:20', N'0 Hours 19 Minutes ', CAST(N'2024-06-11' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (153, 3, CAST(N'2024-06-11' AS Date), N'11:05:32', N'11:07:09', N'0 Hours 2 Minutes ', CAST(N'2024-06-11' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (154, 2, CAST(N'2024-06-11' AS Date), N'11:07:17', N'11:43:27', N'0 Hours 36 Minutes ', CAST(N'2024-06-11' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (155, 1, CAST(N'2024-06-11' AS Date), N'11:20:34', N'11:44:09', N'0 Hours 24 Minutes ', CAST(N'2024-06-11' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (156, 1, CAST(N'2024-06-11' AS Date), N'11:38:35', N'11:44:09', N'0 Hours 6 Minutes ', CAST(N'2024-06-11' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (157, 1, CAST(N'2024-06-11' AS Date), N'11:39:36', N'11:44:09', N'0 Hours 5 Minutes ', CAST(N'2024-06-11' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (158, 2, CAST(N'2024-06-11' AS Date), N'11:39:57', N'11:43:27', N'0 Hours 4 Minutes ', CAST(N'2024-06-11' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (159, 1, CAST(N'2024-06-11' AS Date), N'11:43:46', N'11:44:09', N'0 Hours 1 Minutes ', CAST(N'2024-06-11' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (160, 2, CAST(N'2024-06-11' AS Date), N'11:44:15', N'11:46:31', N'0 Hours 2 Minutes ', CAST(N'2024-06-11' AS Date), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Attendance] ([ID], [EmpId], [Date], [PunchIn], [PunchOut], [WorkingHours], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [ExtraField], [IsActive]) VALUES (161, 1, CAST(N'2024-06-11' AS Date), N'11:46:35', N'11:47:03', N'0 Hours 1 Minutes ', CAST(N'2024-06-11' AS Date), NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Attendance] OFF
GO
SET IDENTITY_INSERT [dbo].[Client] ON 

INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (1, N'ROOTS')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (2, N'HDFC')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (3, N'F2F Foods')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (4, N'test')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (5, N'test1')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (6, N'Test3')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (7, N'test4')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (8, N'test25')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (9, N'test33')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (10, N'test44')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (11, N'test')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (12, N'test111')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (13, N'test33')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (14, N'testt')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (15, N'asdf')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (16, N'asdf')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (17, N'ASDFGHU')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (18, N'ASDFGHJJK')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (19, N'TEST55')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (20, N'TEST')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (21, N'TEST77')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (22, N'test66')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (23, N'asd')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (24, N'test555')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (25, N'test111')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (26, N'njnsdnoa')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (27, N'vdsxg')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (28, N'F2F')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (29, N'hiiii')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (30, N'test88')
INSERT [dbo].[Client] ([ID], [ClientCompany]) VALUES (1011, N'Jeyasudha')
SET IDENTITY_INSERT [dbo].[Client] OFF
GO
SET IDENTITY_INSERT [dbo].[CompanyPolicy] ON 

INSERT [dbo].[CompanyPolicy] ([ID], [Roles], [LeaveLimit], [InternetLimit], [TravelLimit], [IsActive], [ModifiedBy], [ModifiedDate]) VALUES (1, 1, 24, 1500.0000, 1000.0000, 1, 1, CAST(N'2024-06-11' AS Date))
INSERT [dbo].[CompanyPolicy] ([ID], [Roles], [LeaveLimit], [InternetLimit], [TravelLimit], [IsActive], [ModifiedBy], [ModifiedDate]) VALUES (2, 2, 36, 500.0000, 750.0000, 1, 8, CAST(N'2024-05-30' AS Date))
INSERT [dbo].[CompanyPolicy] ([ID], [Roles], [LeaveLimit], [InternetLimit], [TravelLimit], [IsActive], [ModifiedBy], [ModifiedDate]) VALUES (3, 3, 36, 1500.0000, 1000.0000, 1, 8, CAST(N'2024-05-30' AS Date))
INSERT [dbo].[CompanyPolicy] ([ID], [Roles], [LeaveLimit], [InternetLimit], [TravelLimit], [IsActive], [ModifiedBy], [ModifiedDate]) VALUES (4, 4, 24, 1000.0000, 500.0000, 1, 8, CAST(N'2024-05-30' AS Date))
INSERT [dbo].[CompanyPolicy] ([ID], [Roles], [LeaveLimit], [InternetLimit], [TravelLimit], [IsActive], [ModifiedBy], [ModifiedDate]) VALUES (5, 5, 12, 750.0000, 300.0000, 1, 1, CAST(N'2024-06-10' AS Date))
SET IDENTITY_INSERT [dbo].[CompanyPolicy] OFF
GO
SET IDENTITY_INSERT [dbo].[Designations] ON 

INSERT [dbo].[Designations] ([ID], [Designation]) VALUES (1, N'Admin')
INSERT [dbo].[Designations] ([ID], [Designation]) VALUES (2, N'HR')
INSERT [dbo].[Designations] ([ID], [Designation]) VALUES (3, N'Front-End Developer')
INSERT [dbo].[Designations] ([ID], [Designation]) VALUES (4, N'Back-End Developer')
INSERT [dbo].[Designations] ([ID], [Designation]) VALUES (5, N'App Developer')
INSERT [dbo].[Designations] ([ID], [Designation]) VALUES (6, N'UI/UX Developer')
INSERT [dbo].[Designations] ([ID], [Designation]) VALUES (7, N'Tester')
INSERT [dbo].[Designations] ([ID], [Designation]) VALUES (8, N'Marketing')
SET IDENTITY_INSERT [dbo].[Designations] OFF
GO
SET IDENTITY_INSERT [dbo].[DisplaySalaryDetails] ON 

INSERT [dbo].[DisplaySalaryDetails] ([ID], [EmpId], [EmployeeCode], [BasicSalary], [HouseRent], [Conveyance], [OtherAllowance], [ESI], [Tax], [PF], [Others], [Reimbursement], [NetSalary], [CreatedAt], [UpdatedAt]) VALUES (1, 1, N'BYT001', CAST(50000.00 AS Decimal(11, 2)), CAST(10000.00 AS Decimal(11, 2)), CAST(2000.00 AS Decimal(11, 2)), CAST(5000.00 AS Decimal(11, 2)), CAST(1500.00 AS Decimal(11, 2)), CAST(5000.00 AS Decimal(11, 2)), CAST(2500.00 AS Decimal(11, 2)), CAST(1000.00 AS Decimal(11, 2)), CAST(2000.00 AS Decimal(11, 2)), CAST(59000.00 AS Decimal(11, 2)), CAST(N'2024-05-28' AS Date), CAST(N'2024-05-01' AS Date))
INSERT [dbo].[DisplaySalaryDetails] ([ID], [EmpId], [EmployeeCode], [BasicSalary], [HouseRent], [Conveyance], [OtherAllowance], [ESI], [Tax], [PF], [Others], [Reimbursement], [NetSalary], [CreatedAt], [UpdatedAt]) VALUES (2, 2, N'BYT002', CAST(60000.00 AS Decimal(11, 2)), CAST(12000.00 AS Decimal(11, 2)), CAST(2500.00 AS Decimal(11, 2)), CAST(5500.00 AS Decimal(11, 2)), CAST(1800.00 AS Decimal(11, 2)), CAST(6000.00 AS Decimal(11, 2)), CAST(3000.00 AS Decimal(11, 2)), CAST(1500.00 AS Decimal(11, 2)), CAST(2500.00 AS Decimal(11, 2)), CAST(70200.00 AS Decimal(11, 2)), CAST(N'2024-05-28' AS Date), CAST(N'2024-05-01' AS Date))
INSERT [dbo].[DisplaySalaryDetails] ([ID], [EmpId], [EmployeeCode], [BasicSalary], [HouseRent], [Conveyance], [OtherAllowance], [ESI], [Tax], [PF], [Others], [Reimbursement], [NetSalary], [CreatedAt], [UpdatedAt]) VALUES (3, 3, N'BYT003', CAST(45000.00 AS Decimal(11, 2)), CAST(9000.00 AS Decimal(11, 2)), CAST(1500.00 AS Decimal(11, 2)), CAST(4000.00 AS Decimal(11, 2)), CAST(1300.00 AS Decimal(11, 2)), CAST(4500.00 AS Decimal(11, 2)), CAST(2000.00 AS Decimal(11, 2)), CAST(800.00 AS Decimal(11, 2)), CAST(1500.00 AS Decimal(11, 2)), CAST(52400.00 AS Decimal(11, 2)), CAST(N'2024-05-28' AS Date), CAST(N'2024-05-01' AS Date))
INSERT [dbo].[DisplaySalaryDetails] ([ID], [EmpId], [EmployeeCode], [BasicSalary], [HouseRent], [Conveyance], [OtherAllowance], [ESI], [Tax], [PF], [Others], [Reimbursement], [NetSalary], [CreatedAt], [UpdatedAt]) VALUES (4, 4, N'BYT004', CAST(55000.00 AS Decimal(11, 2)), CAST(11000.00 AS Decimal(11, 2)), CAST(2200.00 AS Decimal(11, 2)), CAST(5200.00 AS Decimal(11, 2)), CAST(1600.00 AS Decimal(11, 2)), CAST(5500.00 AS Decimal(11, 2)), CAST(2700.00 AS Decimal(11, 2)), CAST(1200.00 AS Decimal(11, 2)), CAST(2300.00 AS Decimal(11, 2)), CAST(64700.00 AS Decimal(11, 2)), CAST(N'2024-05-28' AS Date), CAST(N'2024-05-01' AS Date))
INSERT [dbo].[DisplaySalaryDetails] ([ID], [EmpId], [EmployeeCode], [BasicSalary], [HouseRent], [Conveyance], [OtherAllowance], [ESI], [Tax], [PF], [Others], [Reimbursement], [NetSalary], [CreatedAt], [UpdatedAt]) VALUES (5, 5, N'BYT005', CAST(48000.00 AS Decimal(11, 2)), CAST(9500.00 AS Decimal(11, 2)), CAST(1800.00 AS Decimal(11, 2)), CAST(4600.00 AS Decimal(11, 2)), CAST(1400.00 AS Decimal(11, 2)), CAST(4800.00 AS Decimal(11, 2)), CAST(2200.00 AS Decimal(11, 2)), CAST(1000.00 AS Decimal(11, 2)), CAST(1800.00 AS Decimal(11, 2)), CAST(56300.00 AS Decimal(11, 2)), CAST(N'2024-05-28' AS Date), CAST(N'2024-05-01' AS Date))
INSERT [dbo].[DisplaySalaryDetails] ([ID], [EmpId], [EmployeeCode], [BasicSalary], [HouseRent], [Conveyance], [OtherAllowance], [ESI], [Tax], [PF], [Others], [Reimbursement], [NetSalary], [CreatedAt], [UpdatedAt]) VALUES (7, 6, N'BYT006', CAST(55000.00 AS Decimal(11, 2)), CAST(11000.00 AS Decimal(11, 2)), CAST(2200.00 AS Decimal(11, 2)), CAST(5200.00 AS Decimal(11, 2)), CAST(1200.00 AS Decimal(11, 2)), CAST(1600.00 AS Decimal(11, 2)), CAST(2750.00 AS Decimal(11, 2)), CAST(2750.00 AS Decimal(11, 2)), CAST(2300.00 AS Decimal(11, 2)), CAST(67400.00 AS Decimal(11, 2)), CAST(N'2024-05-28' AS Date), CAST(N'2024-05-28' AS Date))
INSERT [dbo].[DisplaySalaryDetails] ([ID], [EmpId], [EmployeeCode], [BasicSalary], [HouseRent], [Conveyance], [OtherAllowance], [ESI], [Tax], [PF], [Others], [Reimbursement], [NetSalary], [CreatedAt], [UpdatedAt]) VALUES (9, 7, N'BYT007', CAST(45000.00 AS Decimal(11, 2)), CAST(9000.00 AS Decimal(11, 2)), CAST(1500.00 AS Decimal(11, 2)), CAST(4000.00 AS Decimal(11, 2)), CAST(800.00 AS Decimal(11, 2)), CAST(1300.00 AS Decimal(11, 2)), CAST(2250.00 AS Decimal(11, 2)), CAST(2250.00 AS Decimal(11, 2)), CAST(1500.00 AS Decimal(11, 2)), CAST(54400.00 AS Decimal(11, 2)), CAST(N'2024-05-29' AS Date), CAST(N'2024-05-29' AS Date))
INSERT [dbo].[DisplaySalaryDetails] ([ID], [EmpId], [EmployeeCode], [BasicSalary], [HouseRent], [Conveyance], [OtherAllowance], [ESI], [Tax], [PF], [Others], [Reimbursement], [NetSalary], [CreatedAt], [UpdatedAt]) VALUES (10, 8, N'BYT008', CAST(45000.00 AS Decimal(11, 2)), CAST(9000.00 AS Decimal(11, 2)), CAST(1500.00 AS Decimal(11, 2)), CAST(4000.00 AS Decimal(11, 2)), CAST(800.00 AS Decimal(11, 2)), CAST(1300.00 AS Decimal(11, 2)), CAST(2250.00 AS Decimal(11, 2)), CAST(2250.00 AS Decimal(11, 2)), CAST(1500.00 AS Decimal(11, 2)), CAST(54400.00 AS Decimal(11, 2)), CAST(N'2024-05-29' AS Date), CAST(N'2024-05-29' AS Date))
INSERT [dbo].[DisplaySalaryDetails] ([ID], [EmpId], [EmployeeCode], [BasicSalary], [HouseRent], [Conveyance], [OtherAllowance], [ESI], [Tax], [PF], [Others], [Reimbursement], [NetSalary], [CreatedAt], [UpdatedAt]) VALUES (11, 3, N'BYT003', CAST(48000.00 AS Decimal(11, 2)), CAST(9500.00 AS Decimal(11, 2)), CAST(1800.00 AS Decimal(11, 2)), CAST(4600.00 AS Decimal(11, 2)), CAST(1400.00 AS Decimal(11, 2)), CAST(4800.00 AS Decimal(11, 2)), CAST(2200.00 AS Decimal(11, 2)), CAST(1000.00 AS Decimal(11, 2)), CAST(1800.00 AS Decimal(11, 2)), CAST(56300.00 AS Decimal(11, 2)), CAST(N'2024-05-31' AS Date), CAST(N'2024-05-01' AS Date))
INSERT [dbo].[DisplaySalaryDetails] ([ID], [EmpId], [EmployeeCode], [BasicSalary], [HouseRent], [Conveyance], [OtherAllowance], [ESI], [Tax], [PF], [Others], [Reimbursement], [NetSalary], [CreatedAt], [UpdatedAt]) VALUES (12, 1, N'BYT001', CAST(55000.00 AS Decimal(11, 2)), CAST(11000.00 AS Decimal(11, 2)), CAST(2200.00 AS Decimal(11, 2)), CAST(5200.00 AS Decimal(11, 2)), CAST(1600.00 AS Decimal(11, 2)), CAST(5500.00 AS Decimal(11, 2)), CAST(2700.00 AS Decimal(11, 2)), CAST(1200.00 AS Decimal(11, 2)), CAST(2300.00 AS Decimal(11, 2)), CAST(64700.00 AS Decimal(11, 2)), CAST(N'2024-05-31' AS Date), CAST(N'2024-05-01' AS Date))
INSERT [dbo].[DisplaySalaryDetails] ([ID], [EmpId], [EmployeeCode], [BasicSalary], [HouseRent], [Conveyance], [OtherAllowance], [ESI], [Tax], [PF], [Others], [Reimbursement], [NetSalary], [CreatedAt], [UpdatedAt]) VALUES (45, 2, N'BYT002', CAST(60000.00 AS Decimal(11, 2)), CAST(12000.00 AS Decimal(11, 2)), CAST(2500.00 AS Decimal(11, 2)), CAST(5500.00 AS Decimal(11, 2)), CAST(1800.00 AS Decimal(11, 2)), CAST(6000.00 AS Decimal(11, 2)), CAST(3000.00 AS Decimal(11, 2)), CAST(1500.00 AS Decimal(11, 2)), CAST(2500.00 AS Decimal(11, 2)), CAST(70200.00 AS Decimal(11, 2)), CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date))
INSERT [dbo].[DisplaySalaryDetails] ([ID], [EmpId], [EmployeeCode], [BasicSalary], [HouseRent], [Conveyance], [OtherAllowance], [ESI], [Tax], [PF], [Others], [Reimbursement], [NetSalary], [CreatedAt], [UpdatedAt]) VALUES (1046, 7, N'BYT007', CAST(38000.00 AS Decimal(11, 2)), CAST(2000.00 AS Decimal(11, 2)), CAST(1000.00 AS Decimal(11, 2)), CAST(1000.00 AS Decimal(11, 2)), CAST(0.00 AS Decimal(11, 2)), CAST(1500.00 AS Decimal(11, 2)), CAST(1900.00 AS Decimal(11, 2)), CAST(1900.00 AS Decimal(11, 2)), CAST(200.00 AS Decimal(11, 2)), CAST(34400.00 AS Decimal(11, 2)), CAST(N'2024-06-10' AS Date), CAST(N'2024-06-10' AS Date))
SET IDENTITY_INSERT [dbo].[DisplaySalaryDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeeAccountDetails] ON 

INSERT [dbo].[EmployeeAccountDetails] ([ID], [EmpId], [salary], [BankName], [AccountNumber], [IFSCCode], [PFAccount], [IsActive], [Status], [CreatedDate], [ModifiedDate], [CreatedName], [ModifiedName], [EmployeeCode], [Field2], [Field3]) VALUES (1, 1, CAST(59000.00 AS Decimal(11, 2)), N'SBI', N'135792468', N'SBI1234', N'2538383949748', 1, N'Pending', CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, N'BYT001', NULL, NULL)
INSERT [dbo].[EmployeeAccountDetails] ([ID], [EmpId], [salary], [BankName], [AccountNumber], [IFSCCode], [PFAccount], [IsActive], [Status], [CreatedDate], [ModifiedDate], [CreatedName], [ModifiedName], [EmployeeCode], [Field2], [Field3]) VALUES (2, 2, CAST(65000.00 AS Decimal(11, 2)), N'Union Bank', N'2837389302001828', N'UNI73892', N'09252728282', 1, N'Approved', CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, N'BYT002', NULL, NULL)
INSERT [dbo].[EmployeeAccountDetails] ([ID], [EmpId], [salary], [BankName], [AccountNumber], [IFSCCode], [PFAccount], [IsActive], [Status], [CreatedDate], [ModifiedDate], [CreatedName], [ModifiedName], [EmployeeCode], [Field2], [Field3]) VALUES (3, 3, CAST(35000.00 AS Decimal(11, 2)), N'TMB', N'123456765', N'TMB08933', N'345678998765', 1, N'Pending', CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, N'BYT003', NULL, NULL)
INSERT [dbo].[EmployeeAccountDetails] ([ID], [EmpId], [salary], [BankName], [AccountNumber], [IFSCCode], [PFAccount], [IsActive], [Status], [CreatedDate], [ModifiedDate], [CreatedName], [ModifiedName], [EmployeeCode], [Field2], [Field3]) VALUES (4, 4, CAST(40000.00 AS Decimal(11, 2)), N'TMB', N'1234567890', N'TMB034567234', N'09878987898', 1, N'Pending', CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, N'BYT004', NULL, NULL)
INSERT [dbo].[EmployeeAccountDetails] ([ID], [EmpId], [salary], [BankName], [AccountNumber], [IFSCCode], [PFAccount], [IsActive], [Status], [CreatedDate], [ModifiedDate], [CreatedName], [ModifiedName], [EmployeeCode], [Field2], [Field3]) VALUES (5, 5, CAST(38000.00 AS Decimal(11, 2)), N'TMB', N'45678765443', N'TMB045678909', N'12323456787654', 1, N'Pending', CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, N'BYT005', NULL, NULL)
INSERT [dbo].[EmployeeAccountDetails] ([ID], [EmpId], [salary], [BankName], [AccountNumber], [IFSCCode], [PFAccount], [IsActive], [Status], [CreatedDate], [ModifiedDate], [CreatedName], [ModifiedName], [EmployeeCode], [Field2], [Field3]) VALUES (6, 6, CAST(42000.00 AS Decimal(11, 2)), N'SBI', N'1234567890987', N'SBI5872974', N'458275982394', 1, N'Pending', CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, N'BYT006', NULL, NULL)
INSERT [dbo].[EmployeeAccountDetails] ([ID], [EmpId], [salary], [BankName], [AccountNumber], [IFSCCode], [PFAccount], [IsActive], [Status], [CreatedDate], [ModifiedDate], [CreatedName], [ModifiedName], [EmployeeCode], [Field2], [Field3]) VALUES (7, 7, CAST(38000.00 AS Decimal(11, 2)), N'TMB', N'579832798137', N'TMB5872974', N'287349837493', 1, N'Pending', CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, N'BYT007', NULL, NULL)
INSERT [dbo].[EmployeeAccountDetails] ([ID], [EmpId], [salary], [BankName], [AccountNumber], [IFSCCode], [PFAccount], [IsActive], [Status], [CreatedDate], [ModifiedDate], [CreatedName], [ModifiedName], [EmployeeCode], [Field2], [Field3]) VALUES (8, 8, CAST(35000.00 AS Decimal(11, 2)), N'SBI', N'884348329842', N'SBI239084093', N'428374983243', 1, N'Pending', CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, N'BYT008', NULL, NULL)
INSERT [dbo].[EmployeeAccountDetails] ([ID], [EmpId], [salary], [BankName], [AccountNumber], [IFSCCode], [PFAccount], [IsActive], [Status], [CreatedDate], [ModifiedDate], [CreatedName], [ModifiedName], [EmployeeCode], [Field2], [Field3]) VALUES (9, 9, CAST(43000.00 AS Decimal(11, 2)), N'SBI', N'7598275921', N'SBI8458934', N'09458984754543', 1, N'Pending', CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, N'BYT009', NULL, NULL)
INSERT [dbo].[EmployeeAccountDetails] ([ID], [EmpId], [salary], [BankName], [AccountNumber], [IFSCCode], [PFAccount], [IsActive], [Status], [CreatedDate], [ModifiedDate], [CreatedName], [ModifiedName], [EmployeeCode], [Field2], [Field3]) VALUES (1002, 10, CAST(40000.00 AS Decimal(11, 2)), N'TMB', N'83951698327489', N'TMB98374h', N'85994369', 1, N'Pending', CAST(N'2024-05-21' AS Date), CAST(N'2024-05-21' AS Date), NULL, NULL, N'BYT010', NULL, NULL)
INSERT [dbo].[EmployeeAccountDetails] ([ID], [EmpId], [salary], [BankName], [AccountNumber], [IFSCCode], [PFAccount], [IsActive], [Status], [CreatedDate], [ModifiedDate], [CreatedName], [ModifiedName], [EmployeeCode], [Field2], [Field3]) VALUES (1003, 12, CAST(50000.00 AS Decimal(11, 2)), N'Test', N'123456789012345678', N'Test', N'23456', 1, N'Pending', CAST(N'2024-06-05' AS Date), CAST(N'2024-06-05' AS Date), NULL, NULL, N'BYT012', NULL, NULL)
INSERT [dbo].[EmployeeAccountDetails] ([ID], [EmpId], [salary], [BankName], [AccountNumber], [IFSCCode], [PFAccount], [IsActive], [Status], [CreatedDate], [ModifiedDate], [CreatedName], [ModifiedName], [EmployeeCode], [Field2], [Field3]) VALUES (1004, 13, CAST(70000.00 AS Decimal(11, 2)), N'Test', N'123456789012345678', N'Test', N'2345678', 1, N'Pending', CAST(N'2024-06-05' AS Date), CAST(N'2024-06-05' AS Date), NULL, NULL, N'BYT013', NULL, NULL)
INSERT [dbo].[EmployeeAccountDetails] ([ID], [EmpId], [salary], [BankName], [AccountNumber], [IFSCCode], [PFAccount], [IsActive], [Status], [CreatedDate], [ModifiedDate], [CreatedName], [ModifiedName], [EmployeeCode], [Field2], [Field3]) VALUES (1005, 14, CAST(80000.00 AS Decimal(11, 2)), N'TEST', N'098765432112345678', N'TEST', N'283764', 0, N'Pending', CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[EmployeeAccountDetails] ([ID], [EmpId], [salary], [BankName], [AccountNumber], [IFSCCode], [PFAccount], [IsActive], [Status], [CreatedDate], [ModifiedDate], [CreatedName], [ModifiedName], [EmployeeCode], [Field2], [Field3]) VALUES (1006, 15, CAST(100000.00 AS Decimal(11, 2)), N'test', N'123456789009876543', N'test', N'2436829', 1, N'Pending', CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[EmployeeAccountDetails] ([ID], [EmpId], [salary], [BankName], [AccountNumber], [IFSCCode], [PFAccount], [IsActive], [Status], [CreatedDate], [ModifiedDate], [CreatedName], [ModifiedName], [EmployeeCode], [Field2], [Field3]) VALUES (1007, 16, CAST(40000.00 AS Decimal(11, 2)), N'Test', N'123456789012345678', N'Test', N'12345678', 1, N'Pending', CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[EmployeeAccountDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeeDetails] ON 

INSERT [dbo].[EmployeeDetails] ([ID], [FirstName], [MiddleName], [LastName], [Email], [MobileNumber], [Address], [City], [PinCode], [State], [Country], [Role], [Designation], [Experience], [Gender], [DateOfBirth], [DateOfJoining], [Religion], [Nationality], [MaritalStatus], [ProfileImage], [IsActive], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [Field1], [Field2], [Field3]) VALUES (1, N'Deepak', N'c', N'R', N'deepak@gmail.com', N'7339641770', N'abc', N'CBE', N'234543', N'TN', N'India', 1, 1, 3, N'Male', CAST(N'2024-05-21' AS Date), CAST(N'2024-05-17' AS Date), N'Hindu', N'Indian', N'Married', N'abc', 1, CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[EmployeeDetails] ([ID], [FirstName], [MiddleName], [LastName], [Email], [MobileNumber], [Address], [City], [PinCode], [State], [Country], [Role], [Designation], [Experience], [Gender], [DateOfBirth], [DateOfJoining], [Religion], [Nationality], [MaritalStatus], [ProfileImage], [IsActive], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [Field1], [Field2], [Field3]) VALUES (2, N'Nandha', N'Kumar', N'M', N'nandha@gmail.com', N'6383012127', N'sundrapuram', N'Coimbatore', N'641041', N'Tamil Nadu', N'India', 5, 4, 1, N'Male', CAST(N'2001-09-06' AS Date), CAST(N'2024-05-17' AS Date), N'Hindu', N'Indian', N'Unmarried', N'abc', 1, CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[EmployeeDetails] ([ID], [FirstName], [MiddleName], [LastName], [Email], [MobileNumber], [Address], [City], [PinCode], [State], [Country], [Role], [Designation], [Experience], [Gender], [DateOfBirth], [DateOfJoining], [Religion], [Nationality], [MaritalStatus], [ProfileImage], [IsActive], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [Field1], [Field2], [Field3]) VALUES (3, N'Mithun', N'Selvan', N'J', N'mithun@gmail.com', N'1293309876', N'Cutralam', N'Tenkasi', N'648502', N'Tamil Nadu', N'India', 2, 2, 5, N'Male', CAST(N'2000-11-19' AS Date), CAST(N'2024-05-17' AS Date), N'Hindu', N'Indian', N'Married', N'abc', 1, CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[EmployeeDetails] ([ID], [FirstName], [MiddleName], [LastName], [Email], [MobileNumber], [Address], [City], [PinCode], [State], [Country], [Role], [Designation], [Experience], [Gender], [DateOfBirth], [DateOfJoining], [Religion], [Nationality], [MaritalStatus], [ProfileImage], [IsActive], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [Field1], [Field2], [Field3]) VALUES (4, N'Hemalatha', N'', N'N S', N'hema@gmail.com', N'9887612345', N'thiruvanamalai', N'Onuputhur', N'645789', N'Tamil Nadu', N'India', 5, 5, 3, N'Female', CAST(N'2002-04-16' AS Date), CAST(N'2024-05-17' AS Date), N'Hindu', N'Indian', N'Unmarried', N'abc', 1, CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[EmployeeDetails] ([ID], [FirstName], [MiddleName], [LastName], [Email], [MobileNumber], [Address], [City], [PinCode], [State], [Country], [Role], [Designation], [Experience], [Gender], [DateOfBirth], [DateOfJoining], [Religion], [Nationality], [MaritalStatus], [ProfileImage], [IsActive], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [Field1], [Field2], [Field3]) VALUES (5, N'Nivetha', N'', N'S', N'nive@gmail.com', N'5678008753', N'Ooty', N'Niligiri', N'641027', N'Tamil Nadu', N'India', 5, 8, 1, N'Female', CAST(N'2001-11-19' AS Date), CAST(N'2024-05-17' AS Date), N'Hindu', N'Indian', N'Unmarried', N'abc', 1, CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[EmployeeDetails] ([ID], [FirstName], [MiddleName], [LastName], [Email], [MobileNumber], [Address], [City], [PinCode], [State], [Country], [Role], [Designation], [Experience], [Gender], [DateOfBirth], [DateOfJoining], [Religion], [Nationality], [MaritalStatus], [ProfileImage], [IsActive], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [Field1], [Field2], [Field3]) VALUES (6, N'Vivin', N'Francis', N'J', N'vivin@gmail.com', N'9090653254', N'Podanur', N'Coimbatore', N'641022', N'Tamil Nadu', N'India', 4, 3, 2, N'Male', CAST(N'2001-03-18' AS Date), CAST(N'2024-05-17' AS Date), N'Hindu', N'Indian', N'Unmarried', N'abc', 1, CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[EmployeeDetails] ([ID], [FirstName], [MiddleName], [LastName], [Email], [MobileNumber], [Address], [City], [PinCode], [State], [Country], [Role], [Designation], [Experience], [Gender], [DateOfBirth], [DateOfJoining], [Religion], [Nationality], [MaritalStatus], [ProfileImage], [IsActive], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [Field1], [Field2], [Field3]) VALUES (7, N'Gokul', N'', N'C', N'gokul@gmail.com', N'9677446341', N'3/61 , Pachagoundanvalasu , Solasiramani (Po), Paramathi Velur (Tk)', N'Namakkal', N'637210', N'Tamil Nadu', N'India', 5, 7, 3, N'Male', CAST(N'2001-03-18' AS Date), CAST(N'2024-05-17' AS Date), N'Hindu', N'Indian', N'Unmarried', N'202406070948_Gokul C.jpg', 1, CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[EmployeeDetails] ([ID], [FirstName], [MiddleName], [LastName], [Email], [MobileNumber], [Address], [City], [PinCode], [State], [Country], [Role], [Designation], [Experience], [Gender], [DateOfBirth], [DateOfJoining], [Religion], [Nationality], [MaritalStatus], [ProfileImage], [IsActive], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [Field1], [Field2], [Field3]) VALUES (8, N'Jegadeeshwar', N'', N'T', N'jega@gmail.com', N'896430484', N'Silapuram', N'Tirupur', N'641045', N'Tamil Nadu', N'India', 5, 6, 3, N'Male', CAST(N'2001-06-01' AS Date), CAST(N'2024-05-17' AS Date), N'Hindu', N'Indian', N'Married', N'abc', 1, CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[EmployeeDetails] ([ID], [FirstName], [MiddleName], [LastName], [Email], [MobileNumber], [Address], [City], [PinCode], [State], [Country], [Role], [Designation], [Experience], [Gender], [DateOfBirth], [DateOfJoining], [Religion], [Nationality], [MaritalStatus], [ProfileImage], [IsActive], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [Field1], [Field2], [Field3]) VALUES (9, N'Mani', N'Sankar', N'M', N'mani@gmail.com', N'6380056130', N'Kovilpatti', N'Thoothukudi', N'628501', N'Tamil Nadu', N'India', 5, 7, 3, N'Male', CAST(N'2002-09-20' AS Date), CAST(N'2024-05-17' AS Date), N'Hindu', N'Indian', N'Married', N'abc', 1, CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[EmployeeDetails] ([ID], [FirstName], [MiddleName], [LastName], [Email], [MobileNumber], [Address], [City], [PinCode], [State], [Country], [Role], [Designation], [Experience], [Gender], [DateOfBirth], [DateOfJoining], [Religion], [Nationality], [MaritalStatus], [ProfileImage], [IsActive], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [Field1], [Field2], [Field3]) VALUES (10, N'Meenashi', N'Sundaram', N'M', N'mena@gmail.com', N'8056391763', N'Kovilpatti', N'Thoothukudi', N'628501', N'TamilNadu', N'India', 5, 4, 1, N'Male', CAST(N'2024-05-21' AS Date), CAST(N'2024-05-21' AS Date), N'Hindu', N'Indian', N'Unmarried', N'xyz', 1, CAST(N'2024-05-21' AS Date), CAST(N'2024-05-21' AS Date), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[EmployeeDetails] ([ID], [FirstName], [MiddleName], [LastName], [Email], [MobileNumber], [Address], [City], [PinCode], [State], [Country], [Role], [Designation], [Experience], [Gender], [DateOfBirth], [DateOfJoining], [Religion], [Nationality], [MaritalStatus], [ProfileImage], [IsActive], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [Field1], [Field2], [Field3]) VALUES (12, N'Test', N'Test', N'Test', N'Test@gmail.com', N'6383012127', N'Test', N'Test', N'641017', N'Test', N'Test', 1, 8, 10, N'Male', CAST(N'2024-06-05' AS Date), CAST(N'2024-06-05' AS Date), N'Test', N'Test', N'Test', N'abc', 1, CAST(N'2024-06-05' AS Date), CAST(N'2024-06-05' AS Date), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[EmployeeDetails] ([ID], [FirstName], [MiddleName], [LastName], [Email], [MobileNumber], [Address], [City], [PinCode], [State], [Country], [Role], [Designation], [Experience], [Gender], [DateOfBirth], [DateOfJoining], [Religion], [Nationality], [MaritalStatus], [ProfileImage], [IsActive], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [Field1], [Field2], [Field3]) VALUES (13, N'Jeeva', N'j', N'L', N'Test@gmsil.com', N'6383012127', N'abc', N'Test', N'641019', N'Test', N'Test', 3, 4, 10, N'Male', CAST(N'2024-06-05' AS Date), CAST(N'2024-06-05' AS Date), N'Test', N'Test', N'Test', N'Devops1.png', 1, CAST(N'2024-06-05' AS Date), CAST(N'2024-06-05' AS Date), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[EmployeeDetails] ([ID], [FirstName], [MiddleName], [LastName], [Email], [MobileNumber], [Address], [City], [PinCode], [State], [Country], [Role], [Designation], [Experience], [Gender], [DateOfBirth], [DateOfJoining], [Religion], [Nationality], [MaritalStatus], [ProfileImage], [IsActive], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [Field1], [Field2], [Field3]) VALUES (14, N'TEST', N'TEST', N'TEST', N'test@gmail.com', N'6383012127', N'TEST', N'TEST', N'641017', N'TEST', N'TEST', 2, 2, 10, N'Male', CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), N'TEST', N'TEST', N'TEST', N'd2.jpg', 0, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[EmployeeDetails] ([ID], [FirstName], [MiddleName], [LastName], [Email], [MobileNumber], [Address], [City], [PinCode], [State], [Country], [Role], [Designation], [Experience], [Gender], [DateOfBirth], [DateOfJoining], [Religion], [Nationality], [MaritalStatus], [ProfileImage], [IsActive], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [Field1], [Field2], [Field3]) VALUES (15, N'Pushpa', N'Raj', N'K', N'pushpa@gmail.com', N'7654321890', N'test', N'test', N'641908', N'test', N'test', 5, 6, 19, N'Male', CAST(N'1990-05-09' AS Date), CAST(N'2024-06-07' AS Date), N'test', N'testtest', N'test', N'202406071233_dh2.jpg', 1, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[EmployeeDetails] ([ID], [FirstName], [MiddleName], [LastName], [Email], [MobileNumber], [Address], [City], [PinCode], [State], [Country], [Role], [Designation], [Experience], [Gender], [DateOfBirth], [DateOfJoining], [Religion], [Nationality], [MaritalStatus], [ProfileImage], [IsActive], [CreatedDate], [ModifiedDate], [CreatedBy], [ModifiedBy], [Field1], [Field2], [Field3]) VALUES (16, N'Ebziba', N'K', N'L', N'ebziba@gmail.com', N'6383013137', N'Thoppampatti', N'CBE', N'641017', N'TamilNadu', N'India', 2, 4, 10, N'Male', CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), N'Hindu', N'Indian', N'Single', N'202406071520_d1.jpg', 1, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[EmployeeDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeeLogin] ON 

INSERT [dbo].[EmployeeLogin] ([ID], [EmpId], [UserName], [Password], [IsActive]) VALUES (1, 1, N'BYT001', N'Deepak@123', 1)
INSERT [dbo].[EmployeeLogin] ([ID], [EmpId], [UserName], [Password], [IsActive]) VALUES (2, 2, N'BYT002', N'Nandha@123', 1)
INSERT [dbo].[EmployeeLogin] ([ID], [EmpId], [UserName], [Password], [IsActive]) VALUES (3, 3, N'BYT003', N'Mithun@123', 1)
INSERT [dbo].[EmployeeLogin] ([ID], [EmpId], [UserName], [Password], [IsActive]) VALUES (4, 4, N'BYT004', N'Hema@123', 1)
INSERT [dbo].[EmployeeLogin] ([ID], [EmpId], [UserName], [Password], [IsActive]) VALUES (5, 5, N'BYT005', N'Nivetha@123', 1)
INSERT [dbo].[EmployeeLogin] ([ID], [EmpId], [UserName], [Password], [IsActive]) VALUES (6, 6, N'BYT006', N'Vivin@123', 1)
INSERT [dbo].[EmployeeLogin] ([ID], [EmpId], [UserName], [Password], [IsActive]) VALUES (7, 7, N'BYT007', N'Gokul@123', 1)
INSERT [dbo].[EmployeeLogin] ([ID], [EmpId], [UserName], [Password], [IsActive]) VALUES (8, 8, N'BYT008', N'Jega@123', 1)
INSERT [dbo].[EmployeeLogin] ([ID], [EmpId], [UserName], [Password], [IsActive]) VALUES (9, 9, N'BYT009', N'BYT009', 1)
INSERT [dbo].[EmployeeLogin] ([ID], [EmpId], [UserName], [Password], [IsActive]) VALUES (1002, 10, N'BYT010', N'BYT010', 1)
INSERT [dbo].[EmployeeLogin] ([ID], [EmpId], [UserName], [Password], [IsActive]) VALUES (1003, 12, N'BYT012', N'BYT012', 1)
INSERT [dbo].[EmployeeLogin] ([ID], [EmpId], [UserName], [Password], [IsActive]) VALUES (1004, 13, N'BYT013', N'Newuser@123', 1)
INSERT [dbo].[EmployeeLogin] ([ID], [EmpId], [UserName], [Password], [IsActive]) VALUES (1005, 14, N'BYT014', N'Byt@1234', 0)
INSERT [dbo].[EmployeeLogin] ([ID], [EmpId], [UserName], [Password], [IsActive]) VALUES (1006, 15, N'BYT015', N'BYT015', 1)
INSERT [dbo].[EmployeeLogin] ([ID], [EmpId], [UserName], [Password], [IsActive]) VALUES (1007, 16, N'BYT016', N'BYT016', 1)
SET IDENTITY_INSERT [dbo].[EmployeeLogin] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeePersonalDetails] ON 

INSERT [dbo].[EmployeePersonalDetails] ([ID], [EmpId], [PgCollegeName], [PgStart], [PgEnd], [PgCourse], [UgCollageName], [UgStart], [UgEnd], [UgCourse], [HSCSchoolName], [HSCStart], [HSCEnd], [HSCCourse], [SSLCSchoolName], [SSLCStart], [SSLCEnd], [EmergencyContact], [Relationship], [IsActive], [CreatedDate], [ModifiedDate], [Field1], [Field2], [Field3]) VALUES (1, 1, N'kpr college', CAST(N'2024-06-10' AS Date), CAST(N'2024-06-10' AS Date), N'M.Tech', N'KPR College', CAST(N'2024-06-10' AS Date), CAST(N'2024-06-10' AS Date), N'KPR College', N'SRM', CAST(N'2024-06-10' AS Date), CAST(N'2024-06-10' AS Date), N'Computer Science', N'SVS', CAST(N'2024-06-10' AS Date), CAST(N'2024-06-10' AS Date), N'7469261259', N'Girl Friend', 1, CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, NULL)
INSERT [dbo].[EmployeePersonalDetails] ([ID], [EmpId], [PgCollegeName], [PgStart], [PgEnd], [PgCourse], [UgCollageName], [UgStart], [UgEnd], [UgCourse], [HSCSchoolName], [HSCStart], [HSCEnd], [HSCCourse], [SSLCSchoolName], [SSLCStart], [SSLCEnd], [EmergencyContact], [Relationship], [IsActive], [CreatedDate], [ModifiedDate], [Field1], [Field2], [Field3]) VALUES (2, 2, N'KPR', CAST(N'2024-05-21' AS Date), CAST(N'2024-05-21' AS Date), N'EEE', N'KPR', CAST(N'2024-05-21' AS Date), CAST(N'2024-05-21' AS Date), N'EEE', N'Bishop', CAST(N'2024-05-21' AS Date), CAST(N'2024-05-21' AS Date), N'CS', N'AVB', CAST(N'2024-05-21' AS Date), CAST(N'2024-05-21' AS Date), N'9944869641', N'sister', 1, CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, NULL)
INSERT [dbo].[EmployeePersonalDetails] ([ID], [EmpId], [PgCollegeName], [PgStart], [PgEnd], [PgCourse], [UgCollageName], [UgStart], [UgEnd], [UgCourse], [HSCSchoolName], [HSCStart], [HSCEnd], [HSCCourse], [SSLCSchoolName], [SSLCStart], [SSLCEnd], [EmergencyContact], [Relationship], [IsActive], [CreatedDate], [ModifiedDate], [Field1], [Field2], [Field3]) VALUES (3, 3, N'Hindusthan Engineering collage', CAST(N'2020-06-17' AS Date), CAST(N'2024-05-17' AS Date), N'MCA', N'SNS College', CAST(N'2017-05-17' AS Date), CAST(N'2020-05-17' AS Date), N'BSc', N'NGP School', CAST(N'2015-05-16' AS Date), CAST(N'2016-05-17' AS Date), N'Biology Maths', N'SNS School', CAST(N'2014-05-17' AS Date), CAST(N'2015-05-17' AS Date), N'8056391763', N'Father', 1, CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, NULL)
INSERT [dbo].[EmployeePersonalDetails] ([ID], [EmpId], [PgCollegeName], [PgStart], [PgEnd], [PgCourse], [UgCollageName], [UgStart], [UgEnd], [UgCourse], [HSCSchoolName], [HSCStart], [HSCEnd], [HSCCourse], [SSLCSchoolName], [SSLCStart], [SSLCEnd], [EmergencyContact], [Relationship], [IsActive], [CreatedDate], [ModifiedDate], [Field1], [Field2], [Field3]) VALUES (4, 4, N'Hindusthan Engineering collage', CAST(N'2020-06-17' AS Date), CAST(N'2024-05-17' AS Date), N'MCA', N'Girls College', CAST(N'2017-05-17' AS Date), CAST(N'2020-05-17' AS Date), N'BSc', N'KR School', CAST(N'2015-05-16' AS Date), CAST(N'2016-05-17' AS Date), N'Computer Science', N'NGP School', CAST(N'2014-05-17' AS Date), CAST(N'2015-05-17' AS Date), N'908754313', N'Brother', 1, CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, NULL)
INSERT [dbo].[EmployeePersonalDetails] ([ID], [EmpId], [PgCollegeName], [PgStart], [PgEnd], [PgCourse], [UgCollageName], [UgStart], [UgEnd], [UgCourse], [HSCSchoolName], [HSCStart], [HSCEnd], [HSCCourse], [SSLCSchoolName], [SSLCStart], [SSLCEnd], [EmergencyContact], [Relationship], [IsActive], [CreatedDate], [ModifiedDate], [Field1], [Field2], [Field3]) VALUES (5, 5, N'Hindusthan Engineering collage', CAST(N'2020-06-17' AS Date), CAST(N'2024-05-17' AS Date), N'MCA', N'KPR College', CAST(N'2017-05-17' AS Date), CAST(N'2020-05-17' AS Date), N'BSc', N'NGP School', CAST(N'2015-05-16' AS Date), CAST(N'2016-05-17' AS Date), N'Computer Science', N'Bishop School', CAST(N'2014-05-17' AS Date), CAST(N'2015-05-17' AS Date), N'908754313', N'Sister', 1, CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, NULL)
INSERT [dbo].[EmployeePersonalDetails] ([ID], [EmpId], [PgCollegeName], [PgStart], [PgEnd], [PgCourse], [UgCollageName], [UgStart], [UgEnd], [UgCourse], [HSCSchoolName], [HSCStart], [HSCEnd], [HSCCourse], [SSLCSchoolName], [SSLCStart], [SSLCEnd], [EmergencyContact], [Relationship], [IsActive], [CreatedDate], [ModifiedDate], [Field1], [Field2], [Field3]) VALUES (6, 6, N'Hindusthan Engineering collage', CAST(N'2020-06-17' AS Date), CAST(N'2024-05-17' AS Date), N'MCA', N'Krishna College', CAST(N'2017-05-17' AS Date), CAST(N'2020-05-17' AS Date), N'BSc', N'Nirmal Matha School', CAST(N'2015-05-16' AS Date), CAST(N'2016-05-17' AS Date), N'Computer Science', N'Nirmal Matha School', CAST(N'2014-05-17' AS Date), CAST(N'2015-05-17' AS Date), N'80657832', N'Mother', 1, CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, NULL)
INSERT [dbo].[EmployeePersonalDetails] ([ID], [EmpId], [PgCollegeName], [PgStart], [PgEnd], [PgCourse], [UgCollageName], [UgStart], [UgEnd], [UgCourse], [HSCSchoolName], [HSCStart], [HSCEnd], [HSCCourse], [SSLCSchoolName], [SSLCStart], [SSLCEnd], [EmergencyContact], [Relationship], [IsActive], [CreatedDate], [ModifiedDate], [Field1], [Field2], [Field3]) VALUES (7, 7, N'SNS College', CAST(N'2024-05-01' AS Date), CAST(N'2017-04-04' AS Date), N'Mtech', N'NGP College', CAST(N'2017-05-17' AS Date), CAST(N'2020-05-17' AS Date), N'BSc', N'Nirmal Matha School', CAST(N'2015-05-16' AS Date), CAST(N'2016-05-17' AS Date), N'Computer Science', N'KPR School', CAST(N'2014-05-17' AS Date), CAST(N'2015-05-17' AS Date), N'752938751', N'Father', 1, CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, NULL)
INSERT [dbo].[EmployeePersonalDetails] ([ID], [EmpId], [PgCollegeName], [PgStart], [PgEnd], [PgCourse], [UgCollageName], [UgStart], [UgEnd], [UgCourse], [HSCSchoolName], [HSCStart], [HSCEnd], [HSCCourse], [SSLCSchoolName], [SSLCStart], [SSLCEnd], [EmergencyContact], [Relationship], [IsActive], [CreatedDate], [ModifiedDate], [Field1], [Field2], [Field3]) VALUES (8, 8, N'Hindusthan College', CAST(N'2024-05-01' AS Date), CAST(N'2017-04-04' AS Date), N'Mtech', N'Krishna College', CAST(N'2017-05-17' AS Date), CAST(N'2020-05-17' AS Date), N'BSc', N'Bishop School', CAST(N'2015-05-16' AS Date), CAST(N'2016-05-17' AS Date), N'Computer Science', N'St pauls School', CAST(N'2014-05-17' AS Date), CAST(N'2015-05-17' AS Date), N'837592845', N'Mother', 1, CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, NULL)
INSERT [dbo].[EmployeePersonalDetails] ([ID], [EmpId], [PgCollegeName], [PgStart], [PgEnd], [PgCourse], [UgCollageName], [UgStart], [UgEnd], [UgCourse], [HSCSchoolName], [HSCStart], [HSCEnd], [HSCCourse], [SSLCSchoolName], [SSLCStart], [SSLCEnd], [EmergencyContact], [Relationship], [IsActive], [CreatedDate], [ModifiedDate], [Field1], [Field2], [Field3]) VALUES (9, 9, N'Karpagam collage', CAST(N'2020-06-17' AS Date), CAST(N'2024-05-17' AS Date), N'M.Ed', N'Karpagam College', CAST(N'2017-05-17' AS Date), CAST(N'2020-05-17' AS Date), N'ECE', N'Rathinam School', CAST(N'2015-05-16' AS Date), CAST(N'2016-05-17' AS Date), N'Computer Science', N'Stpauls', CAST(N'2014-05-17' AS Date), CAST(N'2015-05-17' AS Date), N'8056391763', N'Father', 1, CAST(N'2024-05-17' AS Date), CAST(N'2024-05-17' AS Date), NULL, NULL, NULL)
INSERT [dbo].[EmployeePersonalDetails] ([ID], [EmpId], [PgCollegeName], [PgStart], [PgEnd], [PgCourse], [UgCollageName], [UgStart], [UgEnd], [UgCourse], [HSCSchoolName], [HSCStart], [HSCEnd], [HSCCourse], [SSLCSchoolName], [SSLCStart], [SSLCEnd], [EmergencyContact], [Relationship], [IsActive], [CreatedDate], [ModifiedDate], [Field1], [Field2], [Field3]) VALUES (1002, 10, N'LAP College ', CAST(N'2024-05-21' AS Date), CAST(N'2024-05-21' AS Date), N'MTech', N'LAP College', CAST(N'2024-05-21' AS Date), CAST(N'2024-05-21' AS Date), N'BE', N'St pauls', CAST(N'2024-05-21' AS Date), CAST(N'2024-05-21' AS Date), N'Computer Science', N'St Pauls', CAST(N'2024-05-21' AS Date), CAST(N'2024-05-21' AS Date), N'7402109346', N'Father', 1, CAST(N'2024-05-21' AS Date), CAST(N'2024-05-21' AS Date), NULL, NULL, NULL)
INSERT [dbo].[EmployeePersonalDetails] ([ID], [EmpId], [PgCollegeName], [PgStart], [PgEnd], [PgCourse], [UgCollageName], [UgStart], [UgEnd], [UgCourse], [HSCSchoolName], [HSCStart], [HSCEnd], [HSCCourse], [SSLCSchoolName], [SSLCStart], [SSLCEnd], [EmergencyContact], [Relationship], [IsActive], [CreatedDate], [ModifiedDate], [Field1], [Field2], [Field3]) VALUES (1003, 12, N'Test', CAST(N'2024-06-03' AS Date), CAST(N'2024-06-04' AS Date), N'Test', N'Test', CAST(N'2024-06-03' AS Date), CAST(N'2024-06-04' AS Date), N'Test', N'Test', CAST(N'2024-06-03' AS Date), CAST(N'2024-06-04' AS Date), N'Test', N'Test', CAST(N'2024-06-03' AS Date), CAST(N'2024-06-04' AS Date), N'6383012127', N'Test', 1, CAST(N'2024-06-05' AS Date), CAST(N'2024-06-05' AS Date), NULL, NULL, NULL)
INSERT [dbo].[EmployeePersonalDetails] ([ID], [EmpId], [PgCollegeName], [PgStart], [PgEnd], [PgCourse], [UgCollageName], [UgStart], [UgEnd], [UgCourse], [HSCSchoolName], [HSCStart], [HSCEnd], [HSCCourse], [SSLCSchoolName], [SSLCStart], [SSLCEnd], [EmergencyContact], [Relationship], [IsActive], [CreatedDate], [ModifiedDate], [Field1], [Field2], [Field3]) VALUES (1004, 13, N'Test', CAST(N'2024-06-04' AS Date), CAST(N'2024-06-05' AS Date), N'Test', N'Test', CAST(N'2024-06-02' AS Date), CAST(N'2024-06-04' AS Date), N'Test', N'Test', CAST(N'2024-06-03' AS Date), CAST(N'2024-06-03' AS Date), N'Test', N'Test', CAST(N'2024-06-03' AS Date), CAST(N'2024-06-03' AS Date), N'6383012127', N'Test', 1, CAST(N'2024-06-05' AS Date), CAST(N'2024-06-05' AS Date), NULL, NULL, NULL)
INSERT [dbo].[EmployeePersonalDetails] ([ID], [EmpId], [PgCollegeName], [PgStart], [PgEnd], [PgCourse], [UgCollageName], [UgStart], [UgEnd], [UgCourse], [HSCSchoolName], [HSCStart], [HSCEnd], [HSCCourse], [SSLCSchoolName], [SSLCStart], [SSLCEnd], [EmergencyContact], [Relationship], [IsActive], [CreatedDate], [ModifiedDate], [Field1], [Field2], [Field3]) VALUES (1005, 14, N'TEST', CAST(N'2024-06-03' AS Date), CAST(N'2024-06-05' AS Date), N'TEST', N'TEST', CAST(N'2024-06-04' AS Date), CAST(N'2024-06-06' AS Date), N'TEST', N'TEST', CAST(N'2024-06-04' AS Date), CAST(N'2024-06-06' AS Date), N'TEST', N'TEST', CAST(N'2024-06-05' AS Date), CAST(N'2024-06-04' AS Date), N'6383012127', N'TEST', 0, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL, NULL, NULL)
INSERT [dbo].[EmployeePersonalDetails] ([ID], [EmpId], [PgCollegeName], [PgStart], [PgEnd], [PgCourse], [UgCollageName], [UgStart], [UgEnd], [UgCourse], [HSCSchoolName], [HSCStart], [HSCEnd], [HSCCourse], [SSLCSchoolName], [SSLCStart], [SSLCEnd], [EmergencyContact], [Relationship], [IsActive], [CreatedDate], [ModifiedDate], [Field1], [Field2], [Field3]) VALUES (1006, 15, N'test', CAST(N'2024-06-06' AS Date), CAST(N'2024-06-06' AS Date), N'test', N'test', CAST(N'2024-06-04' AS Date), CAST(N'2024-06-06' AS Date), N'test', N'test', CAST(N'2024-06-03' AS Date), CAST(N'2024-06-06' AS Date), N'test', N'test', CAST(N'2024-06-05' AS Date), CAST(N'2024-06-03' AS Date), N'9087654321', N'test', 1, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL, NULL, NULL)
INSERT [dbo].[EmployeePersonalDetails] ([ID], [EmpId], [PgCollegeName], [PgStart], [PgEnd], [PgCourse], [UgCollageName], [UgStart], [UgEnd], [UgCourse], [HSCSchoolName], [HSCStart], [HSCEnd], [HSCCourse], [SSLCSchoolName], [SSLCStart], [SSLCEnd], [EmergencyContact], [Relationship], [IsActive], [CreatedDate], [ModifiedDate], [Field1], [Field2], [Field3]) VALUES (1007, 16, N'Test', CAST(N'2024-06-04' AS Date), CAST(N'2024-06-05' AS Date), N'Test', N'Test', CAST(N'2024-06-03' AS Date), CAST(N'2024-06-05' AS Date), N'Test', N'Test', CAST(N'2024-06-02' AS Date), CAST(N'2024-06-05' AS Date), N'Test', N'Test', CAST(N'2024-06-05' AS Date), CAST(N'2024-06-02' AS Date), N'6383017172', N'Test', 1, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[EmployeePersonalDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Expensetype] ON 

INSERT [dbo].[Expensetype] ([ID], [Expensetypes]) VALUES (1, N'Internet')
INSERT [dbo].[Expensetype] ([ID], [Expensetypes]) VALUES (2, N'Travel')
SET IDENTITY_INSERT [dbo].[Expensetype] OFF
GO
SET IDENTITY_INSERT [dbo].[Holidays] ON 

INSERT [dbo].[Holidays] ([ID], [HolidayName], [Date], [IsActive]) VALUES (1, N'New Year', CAST(N'2024-01-01' AS Date), 1)
INSERT [dbo].[Holidays] ([ID], [HolidayName], [Date], [IsActive]) VALUES (2, N'Pongal', CAST(N'2024-01-15' AS Date), 1)
INSERT [dbo].[Holidays] ([ID], [HolidayName], [Date], [IsActive]) VALUES (3, N'Thiruvalluvar Day', CAST(N'2024-01-16' AS Date), 1)
INSERT [dbo].[Holidays] ([ID], [HolidayName], [Date], [IsActive]) VALUES (4, N'Uzhavar Thirunal', CAST(N'2024-01-17' AS Date), 1)
INSERT [dbo].[Holidays] ([ID], [HolidayName], [Date], [IsActive]) VALUES (5, N'Thai Poosam', CAST(N'2024-01-25' AS Date), 1)
INSERT [dbo].[Holidays] ([ID], [HolidayName], [Date], [IsActive]) VALUES (6, N'Republic Day', CAST(N'2024-01-26' AS Date), 1)
INSERT [dbo].[Holidays] ([ID], [HolidayName], [Date], [IsActive]) VALUES (7, N'Good Friday', CAST(N'2024-03-29' AS Date), 1)
INSERT [dbo].[Holidays] ([ID], [HolidayName], [Date], [IsActive]) VALUES (8, N'Ramzan', CAST(N'2024-04-11' AS Date), 1)
INSERT [dbo].[Holidays] ([ID], [HolidayName], [Date], [IsActive]) VALUES (9, N'May Day', CAST(N'2024-05-01' AS Date), 1)
INSERT [dbo].[Holidays] ([ID], [HolidayName], [Date], [IsActive]) VALUES (10, N'Bakrid', CAST(N'2024-06-17' AS Date), 1)
INSERT [dbo].[Holidays] ([ID], [HolidayName], [Date], [IsActive]) VALUES (11, N'Muharram', CAST(N'2024-07-17' AS Date), 1)
INSERT [dbo].[Holidays] ([ID], [HolidayName], [Date], [IsActive]) VALUES (12, N'Independence Day', CAST(N'2024-08-15' AS Date), 1)
INSERT [dbo].[Holidays] ([ID], [HolidayName], [Date], [IsActive]) VALUES (13, N'Krishna Jayanthi', CAST(N'2024-08-26' AS Date), 1)
INSERT [dbo].[Holidays] ([ID], [HolidayName], [Date], [IsActive]) VALUES (14, N'Vinayakar Chathurthi', CAST(N'2024-09-07' AS Date), 1)
INSERT [dbo].[Holidays] ([ID], [HolidayName], [Date], [IsActive]) VALUES (15, N'Milad-un-Nabi', CAST(N'2024-09-16' AS Date), 1)
INSERT [dbo].[Holidays] ([ID], [HolidayName], [Date], [IsActive]) VALUES (16, N'Gandhi Jayanthi', CAST(N'2024-10-02' AS Date), 1)
INSERT [dbo].[Holidays] ([ID], [HolidayName], [Date], [IsActive]) VALUES (17, N'Ayutha Pooja', CAST(N'2024-10-11' AS Date), 1)
INSERT [dbo].[Holidays] ([ID], [HolidayName], [Date], [IsActive]) VALUES (18, N'Vijaya Dasami', CAST(N'2024-10-12' AS Date), 1)
INSERT [dbo].[Holidays] ([ID], [HolidayName], [Date], [IsActive]) VALUES (19, N'Deepavali', CAST(N'2024-10-31' AS Date), 1)
INSERT [dbo].[Holidays] ([ID], [HolidayName], [Date], [IsActive]) VALUES (20, N'Christmas', CAST(N'2024-12-25' AS Date), 1)
SET IDENTITY_INSERT [dbo].[Holidays] OFF
GO
SET IDENTITY_INSERT [dbo].[LeaveDetails] ON 

INSERT [dbo].[LeaveDetails] ([ID], [EmpId], [StartDate], [EndDate], [LeaveType], [Reason], [Detailedreason], [Status], [CreatedDate], [IsActive]) VALUES (1, 1, CAST(N'2024-06-01' AS Date), CAST(N'2024-06-10' AS Date), 1, N'BirthDay Celeberation', N'Family friends birthday function', N'Rejected', CAST(N'2024-05-24' AS Date), 0)
INSERT [dbo].[LeaveDetails] ([ID], [EmpId], [StartDate], [EndDate], [LeaveType], [Reason], [Detailedreason], [Status], [CreatedDate], [IsActive]) VALUES (2, 1, CAST(N'2024-06-05' AS Date), CAST(N'2024-06-06' AS Date), 2, N'Marriage Function', N'Family friends Marriage function', N'Rejected', CAST(N'2024-05-24' AS Date), 1)
INSERT [dbo].[LeaveDetails] ([ID], [EmpId], [StartDate], [EndDate], [LeaveType], [Reason], [Detailedreason], [Status], [CreatedDate], [IsActive]) VALUES (3, 3, CAST(N'2024-05-15' AS Date), CAST(N'2024-05-16' AS Date), 2, N'Marriage Function', N'Family friends Marriage function', N'Rejected', CAST(N'2024-05-24' AS Date), 1)
INSERT [dbo].[LeaveDetails] ([ID], [EmpId], [StartDate], [EndDate], [LeaveType], [Reason], [Detailedreason], [Status], [CreatedDate], [IsActive]) VALUES (4, 3, CAST(N'2024-05-15' AS Date), CAST(N'2024-05-16' AS Date), 2, N'Marriage Function', N'Family friends Marriage function', N'Rejected', CAST(N'2024-05-24' AS Date), 1)
INSERT [dbo].[LeaveDetails] ([ID], [EmpId], [StartDate], [EndDate], [LeaveType], [Reason], [Detailedreason], [Status], [CreatedDate], [IsActive]) VALUES (5, 2, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-11' AS Date), 2, N'family function', N'Family function in Home Town', N'Approved', CAST(N'2024-05-24' AS Date), 1)
INSERT [dbo].[LeaveDetails] ([ID], [EmpId], [StartDate], [EndDate], [LeaveType], [Reason], [Detailedreason], [Status], [CreatedDate], [IsActive]) VALUES (1003, 1, CAST(N'2024-05-31' AS Date), CAST(N'2024-05-31' AS Date), 2, N'Test', N'Test', N'Rejected', CAST(N'2024-05-31' AS Date), 1)
INSERT [dbo].[LeaveDetails] ([ID], [EmpId], [StartDate], [EndDate], [LeaveType], [Reason], [Detailedreason], [Status], [CreatedDate], [IsActive]) VALUES (1004, 1, CAST(N'2024-06-03' AS Date), CAST(N'2024-06-03' AS Date), 2, N'testing Mithun', N'Checking form submitted', N'Rejected', CAST(N'2024-06-03' AS Date), 1)
INSERT [dbo].[LeaveDetails] ([ID], [EmpId], [StartDate], [EndDate], [LeaveType], [Reason], [Detailedreason], [Status], [CreatedDate], [IsActive]) VALUES (1005, 1, CAST(N'2024-06-04' AS Date), CAST(N'2024-06-05' AS Date), 2, N'Emergency Situation', N'Emergency Situation', N'Rejected', CAST(N'2024-06-04' AS Date), 1)
INSERT [dbo].[LeaveDetails] ([ID], [EmpId], [StartDate], [EndDate], [LeaveType], [Reason], [Detailedreason], [Status], [CreatedDate], [IsActive]) VALUES (1006, 1, CAST(N'2024-06-08' AS Date), CAST(N'2024-06-09' AS Date), 1, N'viral fever', N'viral fever viral fever', N'Pending', CAST(N'2024-06-08' AS Date), 1)
INSERT [dbo].[LeaveDetails] ([ID], [EmpId], [StartDate], [EndDate], [LeaveType], [Reason], [Detailedreason], [Status], [CreatedDate], [IsActive]) VALUES (1007, 1, CAST(N'2024-06-08' AS Date), CAST(N'2024-06-08' AS Date), 2, N'family function', N'family function', N'Pending', CAST(N'2024-06-08' AS Date), 1)
INSERT [dbo].[LeaveDetails] ([ID], [EmpId], [StartDate], [EndDate], [LeaveType], [Reason], [Detailedreason], [Status], [CreatedDate], [IsActive]) VALUES (1008, 1, CAST(N'2024-06-10' AS Date), CAST(N'2024-06-12' AS Date), 1, N'fever', N'fever', N'Pending', CAST(N'2024-06-10' AS Date), 1)
INSERT [dbo].[LeaveDetails] ([ID], [EmpId], [StartDate], [EndDate], [LeaveType], [Reason], [Detailedreason], [Status], [CreatedDate], [IsActive]) VALUES (1009, 1, CAST(N'2024-06-10' AS Date), CAST(N'2024-06-12' AS Date), 1, N'fever', N'fever', N'Pending', CAST(N'2024-06-10' AS Date), 1)
INSERT [dbo].[LeaveDetails] ([ID], [EmpId], [StartDate], [EndDate], [LeaveType], [Reason], [Detailedreason], [Status], [CreatedDate], [IsActive]) VALUES (1010, 2, CAST(N'2024-06-11' AS Date), CAST(N'2024-06-12' AS Date), 1, N'New Test', N'New Test', N'Approved', CAST(N'2024-06-10' AS Date), 1)
INSERT [dbo].[LeaveDetails] ([ID], [EmpId], [StartDate], [EndDate], [LeaveType], [Reason], [Detailedreason], [Status], [CreatedDate], [IsActive]) VALUES (1011, 7, CAST(N'2024-06-13' AS Date), CAST(N'2024-06-15' AS Date), 1, N'Testing', N'Testing', N'Pending', CAST(N'2024-06-10' AS Date), 1)
SET IDENTITY_INSERT [dbo].[LeaveDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[LeaveTypeDetails] ON 

INSERT [dbo].[LeaveTypeDetails] ([ID], [Leavetypename]) VALUES (1, N'Sick Leave')
INSERT [dbo].[LeaveTypeDetails] ([ID], [Leavetypename]) VALUES (2, N'Casual Leave')
SET IDENTITY_INSERT [dbo].[LeaveTypeDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Priority] ON 

INSERT [dbo].[Priority] ([ID], [Priority]) VALUES (1, N'High')
INSERT [dbo].[Priority] ([ID], [Priority]) VALUES (2, N'Medium')
INSERT [dbo].[Priority] ([ID], [Priority]) VALUES (3, N'Low')
SET IDENTITY_INSERT [dbo].[Priority] OFF
GO
SET IDENTITY_INSERT [dbo].[Project] ON 

INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (1, N'Food delivery Website', N'Order food online from FreshMenu. Choose from a wide range of cuisines and categories for food delivery.', CAST(N'2024-06-09' AS Date), CAST(N'2024-04-03' AS Date), 2, 0, NULL, NULL, CAST(N'2024-05-18' AS Date), 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (2, N'Bank Account website', N'Internet banking provides personal and corporate banking services features & electronic payments In Website', CAST(N'2024-10-23' AS Date), CAST(N'2024-03-01' AS Date), 1, 1, NULL, NULL, CAST(N'2024-05-20' AS Date), 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (3, N'Hospital Management System', N'The hospital management system is a computer system that helps manage the information related to healthcare.', CAST(N'2024-04-15' AS Date), CAST(N'2024-05-25' AS Date), 1, 0, NULL, NULL, CAST(N'2024-05-25' AS Date), 1, CAST(N'2024-05-25' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (5, N'test', N'test', CAST(N'2024-05-26' AS Date), CAST(N'2024-05-31' AS Date), 4, 1, NULL, NULL, CAST(N'2024-05-31' AS Date), 0, CAST(N'2024-05-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (6, N'test1', N'test1', CAST(N'2024-05-26' AS Date), CAST(N'2024-05-31' AS Date), 5, 1, NULL, NULL, CAST(N'2024-05-31' AS Date), 0, CAST(N'2024-05-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (7, N'Test3', N'Test3', CAST(N'2024-05-31' AS Date), CAST(N'2024-05-31' AS Date), 6, 1, NULL, NULL, CAST(N'2024-05-31' AS Date), 0, CAST(N'2024-05-31' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (8, N'test4', N'test4', CAST(N'2024-06-03' AS Date), CAST(N'2024-06-03' AS Date), 7, 1, NULL, NULL, CAST(N'2024-06-03' AS Date), 1, CAST(N'2024-06-03' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (9, N'test25', N'test25', CAST(N'2024-06-03' AS Date), CAST(N'2024-06-03' AS Date), 8, 1, NULL, NULL, CAST(N'2024-06-03' AS Date), 1, CAST(N'2024-06-03' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (10, N'test33', N'test33', CAST(N'2024-06-03' AS Date), CAST(N'2024-06-03' AS Date), 9, 1, NULL, NULL, CAST(N'2024-06-03' AS Date), 1, CAST(N'2024-06-03' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (11, N'test44', N'test44', CAST(N'2024-06-03' AS Date), CAST(N'2024-06-03' AS Date), 10, 1, NULL, NULL, CAST(N'2024-06-03' AS Date), 0, CAST(N'2024-06-03' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (13, N'test111', N'test111', CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), 12, 1, NULL, NULL, CAST(N'2024-06-07' AS Date), 0, CAST(N'2024-06-07' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (15, N'testt', N'testt', CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), 14, 1, NULL, NULL, CAST(N'2024-06-07' AS Date), 0, CAST(N'2024-06-07' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (16, N'asdf', N'asdf', CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), 15, 1, NULL, NULL, CAST(N'2024-06-07' AS Date), 1, CAST(N'2024-06-07' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (18, N'ASDFG', N'ASDRFGHJK', CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), 17, 1, NULL, NULL, CAST(N'2024-06-07' AS Date), 1, CAST(N'2024-06-07' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (19, N'SADFGHJK', N'SADFGH', CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), 18, 1, NULL, NULL, CAST(N'2024-06-07' AS Date), 1, CAST(N'2024-06-07' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (20, N'TEST55', N'TEST55', CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), 19, 1, NULL, NULL, CAST(N'2024-06-07' AS Date), 0, CAST(N'2024-06-07' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (22, N'TEST77', N'TEST77', CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), 21, 1, NULL, NULL, CAST(N'2024-06-07' AS Date), 1, CAST(N'2024-06-07' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (23, N'test66', N'test66', CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), 22, 1, NULL, NULL, CAST(N'2024-06-07' AS Date), 0, CAST(N'2024-06-07' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (24, N'asd', N'asd', CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), 23, 1, NULL, NULL, CAST(N'2024-06-07' AS Date), 0, CAST(N'2024-06-07' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (25, N'test555', N'test555', CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), 24, 1, NULL, NULL, CAST(N'2024-06-07' AS Date), 0, CAST(N'2024-06-07' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (27, N'njnsdnoa', N'njnsdnoa', CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), 26, 1, NULL, NULL, CAST(N'2024-06-07' AS Date), 0, CAST(N'2024-06-07' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (28, N'vdsxg', N'vdsxg', CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), 27, 1, NULL, NULL, CAST(N'2024-06-07' AS Date), 0, CAST(N'2024-06-07' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (29, N'F2F', N'F2F', CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), 28, 1, NULL, NULL, CAST(N'2024-06-07' AS Date), 0, CAST(N'2024-06-07' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (30, N'hiiii', N'hiiii', CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), 29, 1, NULL, NULL, CAST(N'2024-06-07' AS Date), 0, CAST(N'2024-06-07' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (31, N'test88', N'test88', CAST(N'2024-06-08' AS Date), CAST(N'2024-06-08' AS Date), 30, 1, NULL, NULL, CAST(N'2024-06-08' AS Date), 0, CAST(N'2024-06-08' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Project] ([ID], [ProjectName], [Description], [DueDate], [ProjectCreatedDate], [ClientID], [Status], [FileUpload], [CompletedDate], [CreatedDate], [IsActive], [ModifiedDate], [CreatedBy], [ModifiedBy], [feild1]) VALUES (1012, N'Hindusthan College Website', N'Website About Hindusthan', CAST(N'2024-07-30' AS Date), CAST(N'2024-06-10' AS Date), 1011, 1, NULL, NULL, CAST(N'2024-06-10' AS Date), 1, CAST(N'2024-06-10' AS Date), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Project] OFF
GO
SET IDENTITY_INSERT [dbo].[ProjectMembers] ON 

INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (3, 5, 1, 5, N'Logo Design', 1)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (4, 2, 1, 5, N'Back-End Table', 1)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (5, 3, 1, 5, N'Testing', 1)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (6, 1, 2, 2, N'Back-End Table', 1)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (7, 6, 2, 4, N'Testing', 1)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (8, 3, 2, 5, N'Logo Design', 1)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (1003, 6, 3, 4, N'Back-End Table', 1)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2003, 9, 3, 5, N'UI Design', 1)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2004, 10, 3, 5, N'Logo Design', 1)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2005, 7, 3, 5, N'Design', 1)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2006, 1, 5, 1, N'test111', 1)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2007, 1, 6, 1, N'test', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2008, 1, 7, 1, N'Test3', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2010, 1, 11, 1, N'test44', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2068, 1, 13, 1, N'test111', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2073, 1, 15, 1, N'testt', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2074, 1, 15, 1, N'testt', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2075, 1, 15, 1, N'testt', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2076, 1, 15, 1, N'testt', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2077, 1, 15, 1, N'testt', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2081, 1, 20, 1, N'TEST55', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2082, 2, 20, 4, N'TEST55', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2083, 3, 20, 2, N'TEST55', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2084, 4, 20, 5, N'TEST55', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2086, 6, 20, 3, N'TEST55', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2090, 10, 20, 4, N'TEST55', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2092, 13, 20, 4, N'TEST55', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2093, 14, 20, 2, N'TEST55', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2095, 1, 5, 1, N'test111', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2096, 2, 5, 4, N'test11', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2097, 3, 5, 2, N'test11', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2098, 4, 5, 5, N'test11', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2100, 6, 5, 3, N'test11', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2104, 10, 5, 4, N'test11', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2106, 13, 5, 4, N'test11', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2107, 14, 5, 2, N'test11', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2109, 1, 5, 1, N'test111', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2110, 2, 5, 4, N'test11', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2111, 3, 5, 2, N'test11', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2112, 4, 5, 5, N'test11', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2114, 6, 5, 3, N'test11', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2118, 10, 5, 4, N'test11', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2120, 13, 5, 4, N'test11', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2121, 14, 5, 2, N'test11', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2123, 1, 5, 1, N'test111', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2124, 2, 5, 4, N'test11', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2125, 3, 5, 2, N'test11', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2126, 4, 5, 5, N'test11', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2128, 4, 23, 5, N'test66', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2130, 3, 23, 2, N'test66', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2131, 1, 24, 1, N'asd', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2132, 2, 24, 4, N'asd', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2133, 3, 24, 2, N'asd', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2134, 4, 24, 5, N'asd', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2135, 2, 25, 4, N'test555', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2136, 1, 25, 1, N'test555', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2137, 3, 25, 2, N'test555', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2139, 2, 13, 4, N'test111', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2140, 3, 13, 2, N'test111', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2141, 4, 13, 5, N'test111', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2142, 1, 13, 1, N'test111', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2143, 4, 27, 5, N'njnsdnoa', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2144, 2, 27, 4, N'njnsdnoa', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2145, 1, 27, 1, N'njnsdnoa', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2147, 1, 28, 1, N'vdsxg', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2149, 2, 29, 4, N'F2F', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2150, 4, 29, 5, N'F2F', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2152, 1, 30, 1, N'hiiii', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2153, 2, 30, 4, N'hiiii', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2154, 3, 30, 2, N'hiiii', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2155, 1, 31, 1, N'test88', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2156, 2, 31, 4, N'test88', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (2157, 3, 31, 2, N'test88', 0)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (3155, 2, 1012, 4, N'Home', 1)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (3156, 3, 1012, 2, N'Home', 1)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (3157, 4, 1012, 5, N'Home', 1)
INSERT [dbo].[ProjectMembers] ([ID], [TeamMembersID], [ProjectID], [Role], [Module], [IsActive]) VALUES (3161, 6, 1012, 3, N'Home', 1)
SET IDENTITY_INSERT [dbo].[ProjectMembers] OFF
GO
SET IDENTITY_INSERT [dbo].[Reasons] ON 

INSERT [dbo].[Reasons] ([id], [problem]) VALUES (1, N'Salary')
INSERT [dbo].[Reasons] ([id], [problem]) VALUES (2, N'Moving New Location')
INSERT [dbo].[Reasons] ([id], [problem]) VALUES (3, N'Not happy with job')
INSERT [dbo].[Reasons] ([id], [problem]) VALUES (4, N'will focus on studies')
INSERT [dbo].[Reasons] ([id], [problem]) VALUES (5, N'personal reason')
SET IDENTITY_INSERT [dbo].[Reasons] OFF
GO
SET IDENTITY_INSERT [dbo].[ReimbursementClaim] ON 

INSERT [dbo].[ReimbursementClaim] ([ID], [EmpId], [Role], [Expense], [BillNo], [BillDate], [BillAmount], [Bill], [Status], [CreatedDate], [IsActive]) VALUES (1089, 1, 2, 2, N'BIL13052024', CAST(N'2024-05-01' AS Date), 750.0000, N'BILLFILE', N'Approved', CAST(N'2024-06-08' AS Date), 1)
INSERT [dbo].[ReimbursementClaim] ([ID], [EmpId], [Role], [Expense], [BillNo], [BillDate], [BillAmount], [Bill], [Status], [CreatedDate], [IsActive]) VALUES (1090, 2, 4, 1, N'BIL14052024', CAST(N'2024-05-04' AS Date), 1000.0000, N'BILLFILE', N'Pending', CAST(N'2024-06-08' AS Date), 1)
INSERT [dbo].[ReimbursementClaim] ([ID], [EmpId], [Role], [Expense], [BillNo], [BillDate], [BillAmount], [Bill], [Status], [CreatedDate], [IsActive]) VALUES (1091, 8, 5, 2, N'BIL18052024', CAST(N'2024-05-06' AS Date), 500.0000, N'BILLFILE', N'Pending', CAST(N'2024-06-08' AS Date), 1)
INSERT [dbo].[ReimbursementClaim] ([ID], [EmpId], [Role], [Expense], [BillNo], [BillDate], [BillAmount], [Bill], [Status], [CreatedDate], [IsActive]) VALUES (1092, 5, 5, 2, N'BIL13052024', CAST(N'2024-05-11' AS Date), 400.0000, N'BILLFILE', N'Pending', CAST(N'2024-06-08' AS Date), 1)
INSERT [dbo].[ReimbursementClaim] ([ID], [EmpId], [Role], [Expense], [BillNo], [BillDate], [BillAmount], [Bill], [Status], [CreatedDate], [IsActive]) VALUES (1093, 4, 5, 1, N'BIL13052024', CAST(N'2024-05-02' AS Date), 300.0000, N'BILLFILE', N'Pending', CAST(N'2024-06-08' AS Date), 1)
INSERT [dbo].[ReimbursementClaim] ([ID], [EmpId], [Role], [Expense], [BillNo], [BillDate], [BillAmount], [Bill], [Status], [CreatedDate], [IsActive]) VALUES (1094, 5, 5, 2, N'BIL13052024', CAST(N'2024-05-13' AS Date), 350.0000, N'BILLFILE', N'Pending', CAST(N'2024-06-08' AS Date), 1)
INSERT [dbo].[ReimbursementClaim] ([ID], [EmpId], [Role], [Expense], [BillNo], [BillDate], [BillAmount], [Bill], [Status], [CreatedDate], [IsActive]) VALUES (1095, 3, 5, 1, N'BIL13052024', CAST(N'2024-05-06' AS Date), 250.0000, N'BILLFILE', N'Pending', CAST(N'2024-06-08' AS Date), 1)
INSERT [dbo].[ReimbursementClaim] ([ID], [EmpId], [Role], [Expense], [BillNo], [BillDate], [BillAmount], [Bill], [Status], [CreatedDate], [IsActive]) VALUES (1096, 1, 2, 2, N'BIL13052024', CAST(N'2024-05-16' AS Date), 550.0000, N'BILLFILE', N'Pending', CAST(N'2024-06-08' AS Date), 1)
INSERT [dbo].[ReimbursementClaim] ([ID], [EmpId], [Role], [Expense], [BillNo], [BillDate], [BillAmount], [Bill], [Status], [CreatedDate], [IsActive]) VALUES (1097, 2, 4, 2, N'BIL13052024', CAST(N'2024-05-11' AS Date), 650.0000, N'BILLFILE', N'Pending', CAST(N'2024-06-08' AS Date), 1)
INSERT [dbo].[ReimbursementClaim] ([ID], [EmpId], [Role], [Expense], [BillNo], [BillDate], [BillAmount], [Bill], [Status], [CreatedDate], [IsActive]) VALUES (1098, 4, 5, 1, N'BIL13052024', CAST(N'2024-05-10' AS Date), 950.0000, N'BILLFILE', N'Pending', CAST(N'2024-06-08' AS Date), 1)
INSERT [dbo].[ReimbursementClaim] ([ID], [EmpId], [Role], [Expense], [BillNo], [BillDate], [BillAmount], [Bill], [Status], [CreatedDate], [IsActive]) VALUES (1099, 6, 5, 1, N'BIL13052024', CAST(N'2024-05-16' AS Date), 450.0000, N'BILLFILE', N'Pending', CAST(N'2024-06-08' AS Date), 1)
INSERT [dbo].[ReimbursementClaim] ([ID], [EmpId], [Role], [Expense], [BillNo], [BillDate], [BillAmount], [Bill], [Status], [CreatedDate], [IsActive]) VALUES (1100, 7, 5, 1, N'BIL13052024', CAST(N'2024-05-08' AS Date), 550.0000, N'BILLFILE', N'Pending', CAST(N'2024-06-08' AS Date), 1)
INSERT [dbo].[ReimbursementClaim] ([ID], [EmpId], [Role], [Expense], [BillNo], [BillDate], [BillAmount], [Bill], [Status], [CreatedDate], [IsActive]) VALUES (1101, 8, 5, 2, N'BIL13052024', CAST(N'2024-05-04' AS Date), 670.0000, N'BILLFILE', N'Pending', CAST(N'2024-06-08' AS Date), 1)
INSERT [dbo].[ReimbursementClaim] ([ID], [EmpId], [Role], [Expense], [BillNo], [BillDate], [BillAmount], [Bill], [Status], [CreatedDate], [IsActive]) VALUES (1102, 7, 5, 1, N'BIL13052024', CAST(N'2024-05-07' AS Date), 750.0000, N'BILLFILE', N'Pending', CAST(N'2024-06-08' AS Date), 1)
INSERT [dbo].[ReimbursementClaim] ([ID], [EmpId], [Role], [Expense], [BillNo], [BillDate], [BillAmount], [Bill], [Status], [CreatedDate], [IsActive]) VALUES (1103, 3, 5, 2, N'BIL13052024', CAST(N'2024-05-13' AS Date), 460.0000, N'BILLFILE', N'Pending', CAST(N'2024-06-08' AS Date), 1)
INSERT [dbo].[ReimbursementClaim] ([ID], [EmpId], [Role], [Expense], [BillNo], [BillDate], [BillAmount], [Bill], [Status], [CreatedDate], [IsActive]) VALUES (1104, 6, 5, 1, N'BIL13052024', CAST(N'2024-05-10' AS Date), 250.0000, N'BILLFILE', N'Pending', CAST(N'2024-06-08' AS Date), 1)
INSERT [dbo].[ReimbursementClaim] ([ID], [EmpId], [Role], [Expense], [BillNo], [BillDate], [BillAmount], [Bill], [Status], [CreatedDate], [IsActive]) VALUES (2073, 7, 5, 2, N'001', CAST(N'2024-06-01' AS Date), 200.0000, N'202406101021_gokul.jpg', N'Pending', CAST(N'2024-06-10' AS Date), 1)
INSERT [dbo].[ReimbursementClaim] ([ID], [EmpId], [Role], [Expense], [BillNo], [BillDate], [BillAmount], [Bill], [Status], [CreatedDate], [IsActive]) VALUES (2074, 3, 2, 2, N'Bill12', CAST(N'2024-06-03' AS Date), 340.0000, N'202406101657_Volunteer.jpg', N'Pending', CAST(N'2024-06-10' AS Date), 1)
INSERT [dbo].[ReimbursementClaim] ([ID], [EmpId], [Role], [Expense], [BillNo], [BillDate], [BillAmount], [Bill], [Status], [CreatedDate], [IsActive]) VALUES (2075, 7, 5, 2, N'002', CAST(N'2024-06-11' AS Date), 205.0000, N'202406111100_Do_Something_Orga_(4)-8GYmJLDLX-transformed.png', N'Pending', CAST(N'2024-06-11' AS Date), 1)
SET IDENTITY_INSERT [dbo].[ReimbursementClaim] OFF
GO
SET IDENTITY_INSERT [dbo].[ResignationTable] ON 

INSERT [dbo].[ResignationTable] ([ID], [EmpId], [LastDateOfWorking], [ReasonForResign], [AdditionalReasonForResign], [Status], [IsActive], [Field2], [Field3]) VALUES (7, 4, CAST(N'2024-06-03' AS Date), N'Salary', N'no', N'Rejected', 1, NULL, NULL)
INSERT [dbo].[ResignationTable] ([ID], [EmpId], [LastDateOfWorking], [ReasonForResign], [AdditionalReasonForResign], [Status], [IsActive], [Field2], [Field3]) VALUES (9, 4, CAST(N'2024-06-14' AS Date), N'Not happy with job, Moving New Location', N'Salary is less', N'Rejected', 1, NULL, NULL)
INSERT [dbo].[ResignationTable] ([ID], [EmpId], [LastDateOfWorking], [ReasonForResign], [AdditionalReasonForResign], [Status], [IsActive], [Field2], [Field3]) VALUES (10, 2, CAST(N'2024-06-08' AS Date), N'personal reason', N'Not', N'Rejected', 1, NULL, NULL)
INSERT [dbo].[ResignationTable] ([ID], [EmpId], [LastDateOfWorking], [ReasonForResign], [AdditionalReasonForResign], [Status], [IsActive], [Field2], [Field3]) VALUES (11, 2, CAST(N'2024-06-13' AS Date), N'Salary', N'no', N'Rejected', 1, NULL, NULL)
INSERT [dbo].[ResignationTable] ([ID], [EmpId], [LastDateOfWorking], [ReasonForResign], [AdditionalReasonForResign], [Status], [IsActive], [Field2], [Field3]) VALUES (12, 2, CAST(N'2024-06-13' AS Date), N'Moving New Location', N'Nill', N'Rejected', 1, NULL, NULL)
INSERT [dbo].[ResignationTable] ([ID], [EmpId], [LastDateOfWorking], [ReasonForResign], [AdditionalReasonForResign], [Status], [IsActive], [Field2], [Field3]) VALUES (13, 2, CAST(N'2024-06-10' AS Date), N'Not happy with job', N'Nill', N'Rejected', 1, NULL, NULL)
INSERT [dbo].[ResignationTable] ([ID], [EmpId], [LastDateOfWorking], [ReasonForResign], [AdditionalReasonForResign], [Status], [IsActive], [Field2], [Field3]) VALUES (14, 2, CAST(N'2024-06-13' AS Date), N'personal reason', N'NA', N'Rejected', 1, NULL, NULL)
INSERT [dbo].[ResignationTable] ([ID], [EmpId], [LastDateOfWorking], [ReasonForResign], [AdditionalReasonForResign], [Status], [IsActive], [Field2], [Field3]) VALUES (1009, 14, CAST(N'2024-06-15' AS Date), N'personal reason, Salary', N'NA', N'Approved', 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ResignationTable] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([ID], [Role]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([ID], [Role]) VALUES (2, N'HR')
INSERT [dbo].[Roles] ([ID], [Role]) VALUES (3, N'HOD')
INSERT [dbo].[Roles] ([ID], [Role]) VALUES (4, N'Team Lead')
INSERT [dbo].[Roles] ([ID], [Role]) VALUES (5, N'Employee')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Task] ON 

INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (1, 2, N'Product Page', CAST(N'2024-06-15' AS Date), 5, 1, 1, NULL, CAST(N'2024-05-29' AS Date), CAST(N'2024-05-29' AS Date), CAST(N'2024-06-08' AS Date))
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (2, 1, N'Product Page', CAST(N'2024-06-15' AS Date), 6, 1, 1, NULL, CAST(N'2024-05-29' AS Date), CAST(N'2024-05-29' AS Date), CAST(N'2024-06-08' AS Date))
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (3, 1, N'Appointment booking', CAST(N'2024-06-23' AS Date), 3, 1, 1, NULL, CAST(N'2024-05-29' AS Date), CAST(N'2024-05-29' AS Date), NULL)
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (4, 2, N'Payment Gateway', CAST(N'2024-05-10' AS Date), 3, 0, 1, NULL, CAST(N'2024-05-29' AS Date), CAST(N'2024-05-29' AS Date), CAST(N'2024-06-02' AS Date))
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (5, 2, N'Web site UI Update', CAST(N'2024-05-03' AS Date), 3, 0, 1, NULL, CAST(N'2024-05-29' AS Date), CAST(N'2024-05-29' AS Date), CAST(N'2024-06-01' AS Date))
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (6, 1, N'Web site logo Design', CAST(N'2024-05-26' AS Date), 3, 1, 1, NULL, CAST(N'2024-05-29' AS Date), CAST(N'2024-05-29' AS Date), NULL)
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (7, 1, N'Web site UI Update', CAST(N'2024-05-30' AS Date), 3, 1, 1, NULL, CAST(N'2024-05-29' AS Date), CAST(N'2024-05-29' AS Date), NULL)
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (8, 1, N'Application logo Design', CAST(N'2024-05-30' AS Date), 3, 0, 1, NULL, CAST(N'2024-05-29' AS Date), CAST(N'2024-05-29' AS Date), CAST(N'2024-06-05' AS Date))
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (9, 2, N'Web site UI Update', CAST(N'2024-06-30' AS Date), 2, 1, 1, NULL, CAST(N'2024-05-29' AS Date), CAST(N'2024-05-29' AS Date), NULL)
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (10, 2, N'Bank Account logo Design', CAST(N'2024-05-15' AS Date), 2, 1, 1, NULL, CAST(N'2024-05-29' AS Date), CAST(N'2024-05-29' AS Date), NULL)
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (11, 2, N'Website Layout Design', CAST(N'2024-05-10' AS Date), 1, 0, 1, NULL, CAST(N'2024-05-29' AS Date), CAST(N'2024-05-29' AS Date), CAST(N'2024-06-07' AS Date))
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (12, 3, N'Product Page', CAST(N'2024-05-29' AS Date), 4, 0, 1, NULL, CAST(N'2024-05-29' AS Date), CAST(N'2024-05-29' AS Date), CAST(N'2024-06-01' AS Date))
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (13, 3, N'Back End Table', CAST(N'2024-05-29' AS Date), 5, 0, 1, NULL, CAST(N'2024-05-29' AS Date), CAST(N'2024-05-29' AS Date), CAST(N'2024-05-23' AS Date))
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (14, 5, N'test', CAST(N'2024-06-15' AS Date), 1, 0, 1, NULL, CAST(N'2024-05-31' AS Date), CAST(N'2024-05-31' AS Date), CAST(N'2024-05-23' AS Date))
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (15, 6, N'test1', CAST(N'2024-06-15' AS Date), 1, 0, 1, NULL, CAST(N'2024-05-31' AS Date), CAST(N'2024-05-31' AS Date), CAST(N'2024-05-23' AS Date))
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (16, 7, N'Test3', CAST(N'2024-05-31' AS Date), 1, 0, 1, NULL, CAST(N'2024-05-31' AS Date), CAST(N'2024-05-31' AS Date), CAST(N'2024-05-23' AS Date))
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (17, 8, N'test4', CAST(N'2024-06-03' AS Date), 1, 0, 1, NULL, CAST(N'2024-06-03' AS Date), CAST(N'2024-06-03' AS Date), CAST(N'2024-05-23' AS Date))
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (18, 9, N'test25', CAST(N'2024-06-03' AS Date), 1, 0, 1, NULL, CAST(N'2024-06-03' AS Date), CAST(N'2024-06-03' AS Date), CAST(N'2024-05-23' AS Date))
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (19, 10, N'test33', CAST(N'2024-06-03' AS Date), 1, 0, 1, NULL, CAST(N'2024-06-03' AS Date), CAST(N'2024-06-03' AS Date), CAST(N'2024-05-23' AS Date))
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (20, 11, N'test44', CAST(N'2024-06-03' AS Date), 1, 0, 1, NULL, CAST(N'2024-06-03' AS Date), CAST(N'2024-06-03' AS Date), CAST(N'2024-06-05' AS Date))
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (22, 13, N'test111', CAST(N'2024-06-07' AS Date), 1, 1, 1, NULL, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL)
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (23, 10, N'test33', CAST(N'2024-06-07' AS Date), 1, 0, 1, NULL, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), CAST(N'2024-06-08' AS Date))
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (24, 15, N'testt', CAST(N'2024-06-07' AS Date), 1, 1, 1, NULL, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL)
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (26, 16, N'asdfgh', CAST(N'2024-06-07' AS Date), 15, 1, 1, NULL, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL)
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (27, 18, N'SWDEFRGTYUJ', CAST(N'2024-06-07' AS Date), 15, 1, 1, NULL, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL)
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (28, 19, N'WADFRGTH', CAST(N'2024-06-01' AS Date), 15, 1, 1, NULL, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL)
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (29, 20, N'TEST55', CAST(N'2024-06-07' AS Date), 15, 1, 1, NULL, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL)
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (30, 5, N'TEST', CAST(N'2024-06-07' AS Date), 15, 1, 1, NULL, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL)
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (31, 22, N'TEST77', CAST(N'2024-06-07' AS Date), 15, 0, 1, NULL, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), CAST(N'2024-06-08' AS Date))
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (32, 23, N'test66', CAST(N'2024-06-07' AS Date), 15, 1, 1, NULL, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL)
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (33, 24, N'asd', CAST(N'2024-06-07' AS Date), 15, 1, 1, NULL, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL)
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (34, 25, N'test555', CAST(N'2024-06-07' AS Date), 15, 1, 1, NULL, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL)
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (35, 13, N'test111', CAST(N'2024-06-07' AS Date), 15, 1, 1, NULL, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL)
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (36, 27, N'njnsdnoa', CAST(N'2024-06-07' AS Date), 15, 1, 1, NULL, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL)
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (37, 28, N'vdsxg', CAST(N'2024-06-07' AS Date), 15, 1, 1, NULL, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL)
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (38, 29, N'F2F', CAST(N'2024-06-07' AS Date), 15, 1, 1, NULL, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL)
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (39, 30, N'hiiii', CAST(N'2024-06-07' AS Date), 16, 1, 1, NULL, CAST(N'2024-06-07' AS Date), CAST(N'2024-06-07' AS Date), NULL)
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (40, 31, N'', CAST(N'2024-06-08' AS Date), 16, 1, 1, NULL, CAST(N'2024-06-08' AS Date), CAST(N'2024-06-08' AS Date), NULL)
INSERT [dbo].[Task] ([ID], [ProjectID], [TaskName], [DueDate], [EmpId], [Status], [IsActive], [CreatedBy], [CreatedDate], [ModifiedDate], [CompletedAt]) VALUES (1040, 1012, N'Build Website', CAST(N'2024-08-01' AS Date), 16, 0, 1, NULL, CAST(N'2024-06-10' AS Date), CAST(N'2024-06-10' AS Date), CAST(N'2024-06-10' AS Date))
SET IDENTITY_INSERT [dbo].[Task] OFF
GO
SET IDENTITY_INSERT [dbo].[Ticket] ON 

INSERT [dbo].[Ticket] ([ID], [EmpId], [TicketSubject], [Priority], [Description], [Status], [RaiseDate], [AttachFile], [CreatedDate], [ModifiedDate], [CreatedBy], [IsActive], [Field1], [Field2], [Field3]) VALUES (1, 1, 3, N'2', N'AC is not working in my cabin', N'Pending', CAST(N'2024-05-22' AS Date), N'ladfalkdkakl', CAST(N'2024-05-22' AS Date), NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[Ticket] ([ID], [EmpId], [TicketSubject], [Priority], [Description], [Status], [RaiseDate], [AttachFile], [CreatedDate], [ModifiedDate], [CreatedBy], [IsActive], [Field1], [Field2], [Field3]) VALUES (2, 2, 1, N'2', N'Lap', N'Declined', CAST(N'2024-05-22' AS Date), N'Image1', CAST(N'2024-05-22' AS Date), NULL, 1, 0, NULL, NULL, NULL)
INSERT [dbo].[Ticket] ([ID], [EmpId], [TicketSubject], [Priority], [Description], [Status], [RaiseDate], [AttachFile], [CreatedDate], [ModifiedDate], [CreatedBy], [IsActive], [Field1], [Field2], [Field3]) VALUES (1002, 3, 1, N'3', N'Keyboard', N'Pending', CAST(N'2024-05-25' AS Date), N'image2', CAST(N'2024-05-25' AS Date), NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[Ticket] ([ID], [EmpId], [TicketSubject], [Priority], [Description], [Status], [RaiseDate], [AttachFile], [CreatedDate], [ModifiedDate], [CreatedBy], [IsActive], [Field1], [Field2], [Field3]) VALUES (1005, 5, 2, N'1', N'adsjh', N'Pending', CAST(N'2024-06-04' AS Date), N'asdkh', CAST(N'2024-06-04' AS Date), NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[Ticket] ([ID], [EmpId], [TicketSubject], [Priority], [Description], [Status], [RaiseDate], [AttachFile], [CreatedDate], [ModifiedDate], [CreatedBy], [IsActive], [Field1], [Field2], [Field3]) VALUES (1007, 1, 4, N'3', N'testing gokul', N'Approved', CAST(N'2024-06-04' AS Date), N'TESTING', CAST(N'2024-06-04' AS Date), NULL, 1, 0, NULL, NULL, NULL)
INSERT [dbo].[Ticket] ([ID], [EmpId], [TicketSubject], [Priority], [Description], [Status], [RaiseDate], [AttachFile], [CreatedDate], [ModifiedDate], [CreatedBy], [IsActive], [Field1], [Field2], [Field3]) VALUES (1008, 1, 3, N'2', N'Testing Nandhu', N'Approved', CAST(N'2024-06-04' AS Date), N'TESTING', CAST(N'2024-06-04' AS Date), NULL, 1, 0, NULL, NULL, NULL)
INSERT [dbo].[Ticket] ([ID], [EmpId], [TicketSubject], [Priority], [Description], [Status], [RaiseDate], [AttachFile], [CreatedDate], [ModifiedDate], [CreatedBy], [IsActive], [Field1], [Field2], [Field3]) VALUES (1009, 1, 1, N'1', N'fghjkl', N'Approved', CAST(N'2024-06-04' AS Date), N'TESTING', CAST(N'2024-06-04' AS Date), NULL, 1, 0, NULL, NULL, NULL)
INSERT [dbo].[Ticket] ([ID], [EmpId], [TicketSubject], [Priority], [Description], [Status], [RaiseDate], [AttachFile], [CreatedDate], [ModifiedDate], [CreatedBy], [IsActive], [Field1], [Field2], [Field3]) VALUES (1010, 6, 1, N'1', N'Testing Today 07-06-2024', N'Declined', CAST(N'2024-06-07' AS Date), N'TESTING', CAST(N'2024-06-07' AS Date), NULL, NULL, 0, NULL, NULL, NULL)
INSERT [dbo].[Ticket] ([ID], [EmpId], [TicketSubject], [Priority], [Description], [Status], [RaiseDate], [AttachFile], [CreatedDate], [ModifiedDate], [CreatedBy], [IsActive], [Field1], [Field2], [Field3]) VALUES (1011, 7, 4, N'3', N'Gokul Testing 07-06-2024
', N'Pending', CAST(N'2024-06-07' AS Date), N'202406071447_VP.jpg', CAST(N'2024-06-07' AS Date), NULL, 7, 1, NULL, NULL, NULL)
INSERT [dbo].[Ticket] ([ID], [EmpId], [TicketSubject], [Priority], [Description], [Status], [RaiseDate], [AttachFile], [CreatedDate], [ModifiedDate], [CreatedBy], [IsActive], [Field1], [Field2], [Field3]) VALUES (2010, 1, 1002, N'3', N'Yokesh Testing', N'Pending', CAST(N'2024-06-08' AS Date), N'202406081518_iD3mLFQm_400x400-removebg-preview.png', CAST(N'2024-06-08' AS Date), NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[Ticket] ([ID], [EmpId], [TicketSubject], [Priority], [Description], [Status], [RaiseDate], [AttachFile], [CreatedDate], [ModifiedDate], [CreatedBy], [IsActive], [Field1], [Field2], [Field3]) VALUES (2011, 5, 1002, N'3', N'Nivetha Testing 10-06-2024', N'Pending', CAST(N'2024-06-10' AS Date), N'202406101204_AIADMK logo.png', CAST(N'2024-06-10' AS Date), NULL, 5, 1, NULL, NULL, NULL)
INSERT [dbo].[Ticket] ([ID], [EmpId], [TicketSubject], [Priority], [Description], [Status], [RaiseDate], [AttachFile], [CreatedDate], [ModifiedDate], [CreatedBy], [IsActive], [Field1], [Field2], [Field3]) VALUES (2013, 5, 1, N'1', N'Nive Testing ', N'Approved', CAST(N'2024-06-10' AS Date), N'202406101209_ap3.jpg', CAST(N'2024-06-10' AS Date), NULL, 5, 0, NULL, NULL, NULL)
INSERT [dbo].[Ticket] ([ID], [EmpId], [TicketSubject], [Priority], [Description], [Status], [RaiseDate], [AttachFile], [CreatedDate], [ModifiedDate], [CreatedBy], [IsActive], [Field1], [Field2], [Field3]) VALUES (2014, 4, 1, N'1', N'Hema Testing 10-06-2024 12:55', N'Approved', CAST(N'2024-06-10' AS Date), N'202406101255_Volunteer.jpg', CAST(N'2024-06-10' AS Date), NULL, 4, 0, NULL, NULL, NULL)
INSERT [dbo].[Ticket] ([ID], [EmpId], [TicketSubject], [Priority], [Description], [Status], [RaiseDate], [AttachFile], [CreatedDate], [ModifiedDate], [CreatedBy], [IsActive], [Field1], [Field2], [Field3]) VALUES (2015, 6, 1, N'1', N'Testing', N'Pending', CAST(N'2024-06-10' AS Date), N'202406101317_Volunteer.jpg', CAST(N'2024-06-10' AS Date), NULL, 6, 1, NULL, NULL, NULL)
INSERT [dbo].[Ticket] ([ID], [EmpId], [TicketSubject], [Priority], [Description], [Status], [RaiseDate], [AttachFile], [CreatedDate], [ModifiedDate], [CreatedBy], [IsActive], [Field1], [Field2], [Field3]) VALUES (2016, 1, 1002, N'3', N'Testing hema', N'Approved', CAST(N'2024-06-10' AS Date), N'202406101644_Do Something Orga (4).png', CAST(N'2024-06-10' AS Date), NULL, 1, 0, NULL, NULL, NULL)
INSERT [dbo].[Ticket] ([ID], [EmpId], [TicketSubject], [Priority], [Description], [Status], [RaiseDate], [AttachFile], [CreatedDate], [ModifiedDate], [CreatedBy], [IsActive], [Field1], [Field2], [Field3]) VALUES (2017, 3, 4, N'3', N'nandha testing', N'Declined', CAST(N'2024-06-10' AS Date), N'202406101651_Do Something Orga (4).png', CAST(N'2024-06-10' AS Date), NULL, 3, 0, NULL, NULL, NULL)
INSERT [dbo].[Ticket] ([ID], [EmpId], [TicketSubject], [Priority], [Description], [Status], [RaiseDate], [AttachFile], [CreatedDate], [ModifiedDate], [CreatedBy], [IsActive], [Field1], [Field2], [Field3]) VALUES (2018, 2, 1, N'1', N'Keyboard not working', N'Approved', CAST(N'2024-06-11' AS Date), N'202406111105_Volunteer.jpg', CAST(N'2024-06-11' AS Date), NULL, 2, 0, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Ticket] OFF
GO
SET IDENTITY_INSERT [dbo].[TicketSubject] ON 

INSERT [dbo].[TicketSubject] ([ID], [Subject], [PriorityId]) VALUES (1, N'System Issue', 1)
INSERT [dbo].[TicketSubject] ([ID], [Subject], [PriorityId]) VALUES (2, N'Internet Issue', 1)
INSERT [dbo].[TicketSubject] ([ID], [Subject], [PriorityId]) VALUES (3, N'AC Issue', 2)
INSERT [dbo].[TicketSubject] ([ID], [Subject], [PriorityId]) VALUES (4, N'Printer Problem', 3)
INSERT [dbo].[TicketSubject] ([ID], [Subject], [PriorityId]) VALUES (1002, N'Others', 3)
SET IDENTITY_INSERT [dbo].[TicketSubject] OFF
GO
SET IDENTITY_INSERT [dbo].[TimeSchedule] ON 

INSERT [dbo].[TimeSchedule] ([ID], [EventName], [Time], [TimeOut], [IsActive]) VALUES (1, N'BREAK 1', N'11:10', N'11:30', 1)
INSERT [dbo].[TimeSchedule] ([ID], [EventName], [Time], [TimeOut], [IsActive]) VALUES (2, N'LUNCH', N'13:00', N'14:00', 1)
INSERT [dbo].[TimeSchedule] ([ID], [EventName], [Time], [TimeOut], [IsActive]) VALUES (3, N'BREAK 2', N'16:20', N'16:40', 1)
INSERT [dbo].[TimeSchedule] ([ID], [EventName], [Time], [TimeOut], [IsActive]) VALUES (4, N'OFFICE IN TIME', N'09:30', N'', 1)
INSERT [dbo].[TimeSchedule] ([ID], [EventName], [Time], [TimeOut], [IsActive]) VALUES (5, N'OFFICE OUT TIME', N'', N'19:30', 1)
INSERT [dbo].[TimeSchedule] ([ID], [EventName], [Time], [TimeOut], [IsActive]) VALUES (6, N'new test', N'12:00', N'12:30', 0)
INSERT [dbo].[TimeSchedule] ([ID], [EventName], [Time], [TimeOut], [IsActive]) VALUES (7, N'new12', N'', N'12:00', 0)
INSERT [dbo].[TimeSchedule] ([ID], [EventName], [Time], [TimeOut], [IsActive]) VALUES (1002, N'Free Time', N'04:10', N'04:15', 0)
INSERT [dbo].[TimeSchedule] ([ID], [EventName], [Time], [TimeOut], [IsActive]) VALUES (1003, N'New', N'11:10', N'11:30', 0)
SET IDENTITY_INSERT [dbo].[TimeSchedule] OFF
GO
ALTER TABLE [dbo].[Attendance] ADD  DEFAULT ('Still Logined') FOR [PunchOut]
GO
ALTER TABLE [dbo].[Attendance] ADD  DEFAULT ('...') FOR [WorkingHours]
GO
ALTER TABLE [dbo].[Attendance] ADD  DEFAULT (CONVERT([date],getdate())) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Attendance] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[CompanyPolicy] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[CompanyPolicy] ADD  DEFAULT (CONVERT([date],getdate())) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[DisplaySalaryDetails] ADD  DEFAULT ((0)) FOR [Reimbursement]
GO
ALTER TABLE [dbo].[DisplaySalaryDetails] ADD  DEFAULT (CONVERT([date],getdate())) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[EmployeeAccountDetails] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[EmployeeAccountDetails] ADD  DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[EmployeeAccountDetails] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[EmployeeAccountDetails] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[EmployeeDetails] ADD  DEFAULT (getdate()) FOR [DateOfJoining]
GO
ALTER TABLE [dbo].[EmployeeDetails] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[EmployeeDetails] ADD  DEFAULT (CONVERT([date],getdate())) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[EmployeeDetails] ADD  DEFAULT (CONVERT([date],getdate())) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[EmployeeLogin] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[EmployeePersonalDetails] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[EmployeePersonalDetails] ADD  DEFAULT (CONVERT([date],getdate())) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[EmployeePersonalDetails] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Holidays] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[LeaveDetails] ADD  DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[LeaveDetails] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[LeaveDetails] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Project] ADD  CONSTRAINT [DEFAULT_Project_ProjectCreatedDate]  DEFAULT (getdate()) FOR [ProjectCreatedDate]
GO
ALTER TABLE [dbo].[Project] ADD  CONSTRAINT [DEFAULT_Project_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Project] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Project] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Project] ADD  CONSTRAINT [DEFAULT_Project_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[ProjectMembers] ADD  CONSTRAINT [DEFAULT_ProjectMembers_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ReimbursementClaim] ADD  DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[ReimbursementClaim] ADD  DEFAULT (CONVERT([date],getdate())) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ReimbursementClaim] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ResignationTable] ADD  DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[ResignationTable] ADD  CONSTRAINT [DEFAULT_ResignationTable_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Task] ADD  CONSTRAINT [DEFAULT_Task_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Task] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Task] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Task] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Ticket] ADD  DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[Ticket] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[TimeSchedule] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD  CONSTRAINT [FK_Attendance] FOREIGN KEY([EmpId])
REFERENCES [dbo].[EmployeeDetails] ([ID])
GO
ALTER TABLE [dbo].[Attendance] CHECK CONSTRAINT [FK_Attendance]
GO
ALTER TABLE [dbo].[CompanyPolicy]  WITH CHECK ADD  CONSTRAINT [FK_Modifiedby] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[EmployeeDetails] ([ID])
GO
ALTER TABLE [dbo].[CompanyPolicy] CHECK CONSTRAINT [FK_Modifiedby]
GO
ALTER TABLE [dbo].[CompanyPolicy]  WITH CHECK ADD  CONSTRAINT [FK_Roles] FOREIGN KEY([Roles])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[CompanyPolicy] CHECK CONSTRAINT [FK_Roles]
GO
ALTER TABLE [dbo].[DisplaySalaryDetails]  WITH CHECK ADD  CONSTRAINT [FK_SalaryDetails_EmpId] FOREIGN KEY([EmpId])
REFERENCES [dbo].[EmployeeDetails] ([ID])
GO
ALTER TABLE [dbo].[DisplaySalaryDetails] CHECK CONSTRAINT [FK_SalaryDetails_EmpId]
GO
ALTER TABLE [dbo].[EmployeeAccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeAccountDetails] FOREIGN KEY([EmpId])
REFERENCES [dbo].[EmployeeDetails] ([ID])
GO
ALTER TABLE [dbo].[EmployeeAccountDetails] CHECK CONSTRAINT [FK_EmployeeAccountDetails]
GO
ALTER TABLE [dbo].[EmployeeDetails]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeDetail] FOREIGN KEY([Designation])
REFERENCES [dbo].[Designations] ([ID])
GO
ALTER TABLE [dbo].[EmployeeDetails] CHECK CONSTRAINT [FK_EmployeeDetail]
GO
ALTER TABLE [dbo].[EmployeeDetails]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeDetails] FOREIGN KEY([Role])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[EmployeeDetails] CHECK CONSTRAINT [FK_EmployeeDetails]
GO
ALTER TABLE [dbo].[EmployeeLogin]  WITH CHECK ADD  CONSTRAINT [FK_EmpId] FOREIGN KEY([EmpId])
REFERENCES [dbo].[EmployeeDetails] ([ID])
GO
ALTER TABLE [dbo].[EmployeeLogin] CHECK CONSTRAINT [FK_EmpId]
GO
ALTER TABLE [dbo].[EmployeePersonalDetails]  WITH CHECK ADD  CONSTRAINT [FK_EmployeePersonaldata] FOREIGN KEY([EmpId])
REFERENCES [dbo].[EmployeeDetails] ([ID])
GO
ALTER TABLE [dbo].[EmployeePersonalDetails] CHECK CONSTRAINT [FK_EmployeePersonaldata]
GO
ALTER TABLE [dbo].[LeaveDetails]  WITH CHECK ADD  CONSTRAINT [PK_empleave] FOREIGN KEY([EmpId])
REFERENCES [dbo].[EmployeeDetails] ([ID])
GO
ALTER TABLE [dbo].[LeaveDetails] CHECK CONSTRAINT [PK_empleave]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Client] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Client] ([ID])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Client]
GO
ALTER TABLE [dbo].[ProjectMembers]  WITH CHECK ADD  CONSTRAINT [FK_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ID])
GO
ALTER TABLE [dbo].[ProjectMembers] CHECK CONSTRAINT [FK_ProjectID]
GO
ALTER TABLE [dbo].[ProjectMembers]  WITH CHECK ADD  CONSTRAINT [FK_ProjectRoles] FOREIGN KEY([Role])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[ProjectMembers] CHECK CONSTRAINT [FK_ProjectRoles]
GO
ALTER TABLE [dbo].[ProjectMembers]  WITH CHECK ADD  CONSTRAINT [FK_TeamMembersID] FOREIGN KEY([TeamMembersID])
REFERENCES [dbo].[EmployeeDetails] ([ID])
GO
ALTER TABLE [dbo].[ProjectMembers] CHECK CONSTRAINT [FK_TeamMembersID]
GO
ALTER TABLE [dbo].[ReimbursementClaim]  WITH CHECK ADD  CONSTRAINT [FK_EmpIdd] FOREIGN KEY([EmpId])
REFERENCES [dbo].[EmployeeDetails] ([ID])
GO
ALTER TABLE [dbo].[ReimbursementClaim] CHECK CONSTRAINT [FK_EmpIdd]
GO
ALTER TABLE [dbo].[ReimbursementClaim]  WITH CHECK ADD  CONSTRAINT [Fk_Expense] FOREIGN KEY([Expense])
REFERENCES [dbo].[Expensetype] ([ID])
GO
ALTER TABLE [dbo].[ReimbursementClaim] CHECK CONSTRAINT [Fk_Expense]
GO
ALTER TABLE [dbo].[ReimbursementClaim]  WITH CHECK ADD  CONSTRAINT [FK_Role] FOREIGN KEY([Role])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[ReimbursementClaim] CHECK CONSTRAINT [FK_Role]
GO
ALTER TABLE [dbo].[ResignationTable]  WITH CHECK ADD  CONSTRAINT [FK_ResignationTable] FOREIGN KEY([EmpId])
REFERENCES [dbo].[EmployeeDetails] ([ID])
GO
ALTER TABLE [dbo].[ResignationTable] CHECK CONSTRAINT [FK_ResignationTable]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_TaskEmpId] FOREIGN KEY([EmpId])
REFERENCES [dbo].[EmployeeDetails] ([ID])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_TaskEmpId]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [Fk_TaskProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ID])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [Fk_TaskProjectID]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_EmployeePersonalDetail] FOREIGN KEY([TicketSubject])
REFERENCES [dbo].[TicketSubject] ([ID])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_EmployeePersonalDetail]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_EmployeePersonalDetails] FOREIGN KEY([EmpId])
REFERENCES [dbo].[EmployeeDetails] ([ID])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_EmployeePersonalDetails]
GO
ALTER TABLE [dbo].[TicketSubject]  WITH CHECK ADD  CONSTRAINT [FK_TicketSubject] FOREIGN KEY([PriorityId])
REFERENCES [dbo].[Priority] ([ID])
GO
ALTER TABLE [dbo].[TicketSubject] CHECK CONSTRAINT [FK_TicketSubject]
GO
/****** Object:  StoredProcedure [dbo].[AllCountDetails]    Script Date: 11-06-2024 11:57:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[AllCountDetails]
AS
BEGIN
SELECT COUNT(ID) AS TotalProject FROM ProjectTable WHERE IsActive=1
SELECT COUNT(ID) AS TotalClient FROM ProjectTable WHERE IsActive=1
SELECT COUNT(ID) AS TotalEmployee FROM EmployeeDetails WHERE IsActive=1
END
GO
/****** Object:  StoredProcedure [dbo].[casualleavechart]    Script Date: 11-06-2024 11:57:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[casualleavechart]
    @EmployeeCode NVARCHAR(50)
AS
BEGIN
    WITH LeaveData AS (
        SELECT 
            ID, 
            EmployeeCode, 
            Leavetypename, 
            LeaveLimit, 
            SUM(leave) AS leavetypecount,
            [Status]
        FROM 
            leavetypeview
        WHERE 
            EmployeeCode = @EmployeeCode 
            AND Leavetypename = 'Sick Leave' 
            AND [Status] = 'Approved'
        GROUP BY 
            ID, EmployeeCode, Leavetypename, LeaveLimit, [Status]
    )
    SELECT 
        COALESCE(ld.ID, dd.ID) AS ID, 
        @EmployeeCode AS EmployeeCode, 
        COALESCE(ld.leavetypecount, 0) AS leavetypecount, 
        'Sick Leave' AS Leavetypename, 
        COALESCE(ld.LeaveLimit, dd.DefaultLeaveLimit) AS LeaveLimit,
        COALESCE(ld.LeaveLimit, dd.DefaultLeaveLimit) - COALESCE(ld.leavetypecount, 0) AS Remainingleave,
        COALESCE(ld.[Status], dd.[Status]) AS [Status]
    FROM 
        (SELECT 
            ID, 
            LeaveLimit AS DefaultLeaveLimit, 
            EmployeeCode,
            [Status]
         FROM 
            leavetypeview 
         WHERE 
            EmployeeCode = @EmployeeCode
            AND [Status] = 'Approved'
         GROUP BY 
            ID, LeaveLimit, EmployeeCode, [Status]) dd
    LEFT JOIN 
        LeaveData ld
        ON dd.ID = ld.ID AND dd.EmployeeCode = ld.EmployeeCode
            AND dd.[Status] = 'Approved'
END;
GO
/****** Object:  StoredProcedure [dbo].[CheckPriority]    Script Date: 11-06-2024 11:57:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[CheckPriority] (@TicketSubject int,@ticketid varchar(10))
AS
BEGIN
    UPDATE Ticket
    SET Priority = CASE
        WHEN @TicketSubject = 1 THEN 1
        WHEN @TicketSubject = 2 THEN 1
        WHEN @TicketSubject = 3 THEN 2
        WHEN @TicketSubject = 4 THEN 3
    END
    WHERE TicketId = @ticketid;
END
GO
/****** Object:  StoredProcedure [dbo].[deletevalue]    Script Date: 11-06-2024 11:57:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[deletevalue] @deleteid NVARCHAR(30)
AS

DELETE ResignationTable WHERE EmpId=@deleteid;
GO
/****** Object:  StoredProcedure [dbo].[GetLeaveDataByEmployeeCode]    Script Date: 11-06-2024 11:57:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetLeaveDataByEmployeeCode]
    @EmployeeCode NVARCHAR(50)
AS
BEGIN
    WITH LeaveData AS (
        SELECT 
            ID, 
            EmployeeCode, 
            Leavetypename, 
            LeaveLimit, 
            SUM(leave) AS leavetypecount
        FROM 
            leavetypeview
        WHERE 
            EmployeeCode = @EmployeeCode 
            AND Leavetypename = 'Sick Leave' 
            AND [Status] = 'Approved'
        GROUP BY 
            ID, EmployeeCode, Leavetypename, LeaveLimit
    )
    SELECT 
        COALESCE(ld.ID, dd.ID) AS ID,
        @EmployeeCode AS EmployeeCode,
        COALESCE(ld.leavetypecount, 0) AS leavetypecount, 
        'Sick Leave' AS Leavetypename, 
        COALESCE(ld.LeaveLimit, dd.DefaultLeaveLimit) AS LeaveLimit,
        COALESCE(ld.LeaveLimit, dd.DefaultLeaveLimit) - COALESCE(ld.leavetypecount, 0) AS Remainingleave
    FROM 
        (SELECT 
            ID, 
            LeaveLimit AS DefaultLeaveLimit,
            EmployeeCode
         FROM 
            leavetypeview
         WHERE 
            EmployeeCode = @EmployeeCode
         GROUP BY 
            ID, LeaveLimit, EmployeeCode) dd
        LEFT JOIN 
        LeaveData ld
        ON dd.ID = ld.ID AND dd.EmployeeCode = ld.EmployeeCode;
END;
GO
/****** Object:  StoredProcedure [dbo].[LeaveDetailsType]    Script Date: 11-06-2024 11:57:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[LeaveDetailsType]
@EmployeeCode NVARCHAR(50)
AS
BEGIN
    SELECT 
        E.[ID], 
        E.[EmployeeCode], 
        E.[FirstName], 
        R.[Role], 
        ISNULL(SUM(L1.[NoOfDays]), 0) AS TakenLeave, 
        C.[LeaveLimit], 
        1 AS LeaveType, 
        'Sick Leave' as LeaveTypename
    FROM 
        EmployeeDetails AS E 
        INNER JOIN CompanyPolicy AS C ON E.[Role] = C.[Roles]
        INNER JOIN Roles AS R ON R.[ID] = E.[Role]
        LEFT JOIN LeaveDetails AS L1 ON E.[ID] = L1.[EmpId] AND L1.[Status] = 'Approved' AND L1.[LeaveType] = 1
    WHERE 
        E.[EmployeeCode] = @EmployeeCode
    GROUP BY 
        E.[ID], E.[EmployeeCode], E.[FirstName], R.[Role], C.[LeaveLimit]

    UNION ALL

    SELECT 
        E.[ID], 
        E.[EmployeeCode], 
        E.[FirstName], 
        R.[Role], 
        ISNULL(SUM(L1.[NoOfDays]), 0) AS TakenLeave, 
        C.[LeaveLimit], 
        2 AS LeaveType, 
        'Casual Leave' as LeaveTypename
    FROM 
        EmployeeDetails AS E 
        INNER JOIN CompanyPolicy AS C ON E.[Role] = C.[Roles]
        INNER JOIN Roles AS R ON R.[ID] = E.[Role]
        LEFT JOIN LeaveDetails AS L1 ON E.[ID] = L1.[EmpId] AND L1.[Status] = 'Approved' AND L1.[LeaveType] = 2
    WHERE 
        E.[EmployeeCode] = @EmployeeCode
    GROUP BY 
        E.[ID], E.[EmployeeCode], E.[FirstName], R.[Role], C.[LeaveLimit]
END
GO
/****** Object:  StoredProcedure [dbo].[PayslipDetails]    Script Date: 11-06-2024 11:57:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create   PROCEDURE [dbo].[PayslipDetails](@EmployeeCode varchar(10))
As
BEGIN
SELECT Emp.[ID],Emp.[EmployeeCode],CONCAT(Emp.[FirstName],'', Emp.[MiddleName],'',Emp.[LastName])As FullName,Desig.[Designation],Emp.[DateOfJoining],AC.[BankName],AC.[AccountNumber],AC.[IFSCCode],
    [BasicPay] ,
    [HouseRent] ,
    [Conveyance] ,
    [OtherAllowance] ,
    [ESI] ,
    [Tax] ,
    [PF],
    [Others]
from EmployeeDetails as Emp
join Designations as Desig
ON Desig.ID=Emp.Designation
join EmployeeAccountDetails As AC
on Ac.EmpId=Emp.ID
join MonthlySalary As MS
ON MS.Id=Emp.ID
WHERE Emp.EmployeeCode=@EmployeeCode
END

GO
/****** Object:  StoredProcedure [dbo].[PersonalHistory]    Script Date: 11-06-2024 11:57:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[PersonalHistory] @EmpCode NVARCHAR(30)
AS
 SELECT ReimbursementAllclaim.ID,
        ReimbursementAllclaim.BillNo,
        ReimbursementAllclaim.BillDate,
        ReimbursementAllclaim.Expensetypes,
        ReimbursementAllclaim.[Status],
        ReimbursementAllclaim.BillAmount

FROM ReimbursementAllclaim

WHERE EmployeeCode = @EmpCode;
GO
/****** Object:  StoredProcedure [dbo].[PunchOutUpdate]    Script Date: 11-06-2024 11:57:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PunchOutUpdate](@EmpId int)
AS
UPDATE Attendance SET PunchOut = convert(varchar, getdate(), 8) WHERE EmpId=@EmpId AND PunchOut='Still Logined';
UPDATE Attendance SET WorkingHours = 
    CASE
        WHEN PunchOut = 'Still Logined' THEN 'Still Logined'
        ELSE 
            CONVERT(NVARCHAR(20), 
                CONVERT(INT, 
                    DATEDIFF(MINUTE, CONVERT(DATETIME, PunchIn, 120), CONVERT(DATETIME, PunchOut, 120)) / 60)) + ' Hours ' + 
            CONVERT(NVARCHAR(20), 
                DATEDIFF(MINUTE, CONVERT(DATETIME, PunchIn, 120), CONVERT(DATETIME, PunchOut, 120)) % 60) + ' Minutes ' 
    END
WHERE EmpId = @EmpId;
GO
/****** Object:  StoredProcedure [dbo].[SaveProject]    Script Date: 11-06-2024 11:57:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveProject] 
        @SpClientCompany varchar(30),
        @SpProjectName NVARCHAR(30),
        @SpDescription NVARCHAR(120),
        @SpDueDate Date,
        @SpTaskName NVARCHAR(60),
        @SpTaskDueDate DATE,
        @spEmpId INT
AS
    INSERT INTO Client (ClientCompany) VALUES (@SpClientCompany);

    INSERT INTO Project (ProjectName,[Description], DueDate, ClientID)
    VALUES (@SpProjectName,@SpDescription, @SpDueDate,(SELECT ID From Client Where ClientCompany = @SpClientCompany));

    INSERT INTO Task (ProjectID, TaskName, DueDate, EmpId )
    VALUES ((SELECT ID From Project Where ProjectName = @SpProjectName),@SpTaskName,@SpTaskDueDate,@spEmpId);
GO
/****** Object:  StoredProcedure [dbo].[SpTeamMembers]    Script Date: 11-06-2024 11:57:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SpTeamMembers] 
        @SpProjectName NVARCHAR(30),
        @spTeamMembersID INT,
        @SpModule VARCHAR(15),
        @spRole INT 
AS
    INSERT INTO ProjectMembers(TeamMembersID,ProjectID,[Role],Module)
    VALUES(@spTeamMembersID,(SELECT ID From Project Where ProjectName = @SpProjectName),@spRole,@SpModule);
GO
/****** Object:  StoredProcedure [dbo].[TotalTicketCount]    Script Date: 11-06-2024 11:57:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[TotalTicketCount]
as
SELECT COUNT(Status) AS NewTicket FROM TicketRaised  
SELECT COUNT(Status) AS PendingTicket FROM TicketRaised where Status='Pending' 
SELECT COUNT(Status) AS SolvedTicket FROM TicketRaised where Not Status='Pending' 
GO
/****** Object:  StoredProcedure [dbo].[UpdateAll]    Script Date: 11-06-2024 11:57:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[UpdateAll] 
    @date NVARCHAR(30), 
    @PunchIn NVARCHAR(30), 
    @PunchOut NVARCHAR(30), 
    @Id INT
AS
BEGIN
    UPDATE Attendance 
    SET date = @date, 
        PunchIn = @PunchIn, 
        PunchOut = @PunchOut 
    WHERE Id=@Id;

    UPDATE Attendance 
    SET WorkingHours = 
        CASE
            WHEN PunchOut = 'Still Logined' THEN 'Still Logined'
            ELSE 
                CONVERT(NVARCHAR(20), CONVERT(INT, DATEDIFF(MINUTE, CONVERT(DATETIME, PunchIn, 120), CONVERT(DATETIME, PunchOut, 120)) / 60)) + ' Hours ' + 
                CONVERT(NVARCHAR(20), DATEDIFF(MINUTE, CONVERT(DATETIME, PunchIn, 120), CONVERT(DATETIME, PunchOut, 120)) % 60) + ' Minutes ' 
        END
    WHERE Id = @Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[updatevalues]    Script Date: 11-06-2024 11:57:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[updatevalues] @updateisactive nvarchar(30)
AS
UPDATE EmployeeLogin set IsActive=0 WHERE EmpId=@updateisactive 
UPDATE EmployeeDetails set IsActive=0 WHERE ID=@updateisactive 
UPDATE EmployeePersonalDetails SET IsActive=0 WHERE EmpId=@updateisactive 
UPDATE EmployeeAccountDetails SET IsActive=0 WHERE EmpId=@updateisactive 
UPDATE ResignationTable SET IsActive=0 WHERE EmpId=@updateisactive
UPDATE ResignationTable SET [Status]='Approved' WHERE EmpId=@updateisactive;
GO
/****** Object:  StoredProcedure [dbo].[updatevalues1]    Script Date: 11-06-2024 11:57:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[updatevalues1] @updateisactive1 nvarchar(30)
AS
UPDATE EmployeeLogin set IsActive=1 WHERE EmpId=@updateisactive1 
UPDATE EmployeeDetails set IsActive=1 WHERE ID=@updateisactive1 
UPDATE EmployeePersonalDetails SET IsActive=1 WHERE EmpId=@updateisactive1
UPDATE EmployeeAccountDetails SET IsActive=1 WHERE EmpId=@updateisactive1
UPDATE ResignationTable SET IsActive=1 WHERE EmpId=@updateisactive1;
UPDATE ResignationTable SET [Status]='Rejected' WHERE EmpId=@updateisactive1;

EXEC updatevalues1 @updateisactive1=2;
GO
/****** Object:  Trigger [dbo].[Login]    Script Date: 11-06-2024 11:57:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   TRIGGER [dbo].[Login] ON [dbo].[EmployeeDetails] FOR INSERT 
AS 
BEGIN
INSERT INTO EmployeeLogin([EmpId],[UserName],[Password])
SELECT ID,EmployeeCode,EmployeeCode FROM inserted
END
GO
ALTER TABLE [dbo].[EmployeeDetails] ENABLE TRIGGER [Login]
GO
USE [master]
GO
ALTER DATABASE [HRMS] SET  READ_WRITE 
GO
