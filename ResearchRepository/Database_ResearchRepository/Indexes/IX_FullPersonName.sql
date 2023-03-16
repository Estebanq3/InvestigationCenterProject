CREATE INDEX index_full_person_name
ON Person(Email)
INCLUDE (FirstName, FirstLastName, SecondLastName)