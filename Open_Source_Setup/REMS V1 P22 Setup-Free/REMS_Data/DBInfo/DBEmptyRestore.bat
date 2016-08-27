cd /D C:\Program Files (x86)\PostgreSQL\9.3\bin\

pg_restore.exe --host localhost  --port 5433 --username postgres --clean --schema-only --dbname "bonades_live" --verbose "C:\1_DESIGNS\MYAPPS\Enterprise_Management_System\Enterprise_Management_System\bin\Debug\prereq\test_database.backup"

xcopy "C:\1_DESIGNS\MYAPPS\Enterprise_Management_System\Enterprise_Management_System\bin\Debug\prereq\Images\*.*" "C:\Databases\bonades_live\" /E /I /-Y /F /C

PAUSE
