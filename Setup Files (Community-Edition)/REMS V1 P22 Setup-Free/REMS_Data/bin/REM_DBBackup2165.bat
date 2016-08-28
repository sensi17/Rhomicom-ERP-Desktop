

 @echo off
SET datestr=%Date:~0,4%%Date:~5,2%%Date:~8,2%%TIME:~0,2%%TIME:~3,2%%TIME:~6,2%%TIME:~9,3% 
   echo datestr is %datestr%
    
   set BACKUP_FILE=C:\1_DESIGNS\test_database_%datestr%.backup
   echo backup file name is %BACKUP_FILE%
   SET PGPASSWORD=Rho201410p
   echo on
cd /D C:\Program Files (x86)\PostgreSQL\9.3\bin\

pg_dump.exe --host 192.168.56.250 --port 5432 --username postgres --format tar --blobs --verbose --file "%BACKUP_FILE%" test_database

forfiles -p "C:\1_DESIGNS" -s -m *.* -d -5 -c "cmd /c del @path"
