cd /D C:\Program Files\PostgreSQL\9.1\bin

pg_restore.exe --host localhost --port 5432 --username postgres --create --dbname "rho_prod17apr2013131834" --verbose "C:\Documents and Settings\Administrator\Desktop\rho_prod17apr2013131751.backup"

PAUSE
