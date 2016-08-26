

 @echo off
SET datestr=%Date:~0,4%%Date:~5,2%%Date:~8,2%%TIME:~0,2%%TIME:~3,2%%TIME:~6,2%%TIME:~9,3% 
   echo datestr is %datestr%
    
   set BACKUP_FILE=C:\Databases\obaa_live\DB_Backups\obaa_live_%datestr%.backup
   echo backup file name is %BACKUP_FILE%
   SET PGPASSWORD=Rho201410p
   echo on
cd /D C:\Program Files (x86)\PostgreSQL\9.3\bin\

pg_dump.exe --host localhost --port 5433 --username postgres --format tar --blobs --verbose --file "%BACKUP_FILE%" obaa_live

forfiles -p "C:\Databases\obaa_live\DB_Backups" -s -m *.* -d -5 -c "cmd /c del @path"
