

 @echo off
   for /f "tokens=1-4 delims=/ " %%i in ("%date%") do (
     set dow=%%i
     set month=%%j
     set day=%%k
     set year=%%l
   )
   set datestr=%month%_%day%_%year%
   echo datestr is %datestr%
    
   set BACKUP_FILE=C:\Databases\test_database\DB_Backups\test_database_%datestr%.backup
   echo backup file name is %BACKUP_FILE%
   SET PGPASSWORD=Rhemitech2015
   echo on
C:\Program Files (x86)\PostgreSQL\9.1\binpg_dump.exe --host localhost --port 5432 --username postgres --format tar --blobs --verbose --file %BACKUP_FILE% test_database
