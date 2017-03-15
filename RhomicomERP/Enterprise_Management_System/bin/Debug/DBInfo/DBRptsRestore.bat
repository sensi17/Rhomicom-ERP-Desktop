cd /D C:\Program Files (x86)\PostgreSQL\9.3\bin\

pg_restore.exe --host localhost  --port 5432 --username postgres --clean --dbname "earthmovers_live" --verbose "C:\RhomicomERP_V1\prereq\sample_rpts.backup"



PAUSE
