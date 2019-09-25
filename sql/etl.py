from sqlalchemy import create_engine

connection_string = "root:password@localhost/test"
engine = create_engine(f'mysql://{connection_string}')

 # Confirm tables
tables = engine.table_names()

print(tables)
