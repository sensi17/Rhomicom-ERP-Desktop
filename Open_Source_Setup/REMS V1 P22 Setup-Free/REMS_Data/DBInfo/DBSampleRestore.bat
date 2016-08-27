cd /D C:\Program Files (x86)\PostgreSQL\9.3\bin\

pg_restore.exe --host localhost  --port 5433 --username postgres --clean --schema-only --dbname "obaa" --verbose "C:\1_DESIGNS\MYAPPS\Enterprise_Management_System\Enterprise_Management_System\bin\Debug\prereq\test_database.backup"

pg_restore.exe --host localhost  --port 5433 --username postgres --data-only --dbname "obaa" --verbose "C:\Users\richard.adjei-mensah\Desktop\obaa_live_20151127 7032526 .backup"

xcopy "C:\1_DESIGNS\MYAPPS\Enterprise_Management_System\Enterprise_Management_System\bin\Debug\prereq\Images\*.*" "C:\1_DESIGNS\MYAPPS\Enterprise_Management_System\Enterprise_Management_System\bin\Debug\Images\obaa\" /E /I /-Y /F /C

PAUSE
