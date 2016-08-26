cd /D C:\Program Files (x86)\PostgreSQL\9.3\bin\

pg_restore.exe --host 192.168.1.252  --port 5432 --username postgres --clean --schema-only --dbname "rhomicom_live" --verbose "C:\1_DESIGNS\MYAPPS\VS2013_PROJS\RhomicomERP\Enterprise_Management_System\bin\Debug\prereq\test_database.backup"

pg_restore.exe --host 192.168.1.252  --port 5432 --username postgres --data-only --dbname "rhomicom_live" --verbose "C:\Users\richard.adjei-mensah\Desktop\rhomicom.backup"

xcopy "C:\1_DESIGNS\MYAPPS\VS2013_PROJS\RhomicomERP\Enterprise_Management_System\bin\Debug\prereq\Images\*.*" "C:\1_DESIGNS\MYAPPS\VS2013_PROJS\RhomicomERP\Enterprise_Management_System\bin\Debug\Images\rhomicom_live\" /E /I /-Y /F /C

PAUSE
