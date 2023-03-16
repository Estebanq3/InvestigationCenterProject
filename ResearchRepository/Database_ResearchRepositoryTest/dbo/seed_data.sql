DELETE FROM PublicationPartOfTesis;
DELETE FROM ProjectAsociatedToPublication;
DELETE FROM ReferenceListPublication;
DELETE FROM Publication;
DELETE FROM InvestigationProject;
DELETE FROM Thesis;
DELETE FROM Person;
DELETE FROM Collaborator;
DELETE FROM Investigator;
DELETE FROM CollaboratorPartOfGroup;
DELETE FROM AcademicProfile;
DELETE FROM AcademicUnit;
DELETE FROM PersonsBelongsToUniversity;
DELETE FROM PersonWorksForUnit;
DELETE FROM University;
DELETE FROM ResearchArea;
DELETE FROM WorkWithUs;

SET IDENTITY_INSERT [dbo].[ResearchArea] ON;

MERGE INTO [dbo].[ResearchArea] AS TARGET
USING
(VALUES
(1, 'Ciencias de la Computación', NULL),
(2, 'Tecnologías de la información', NULL),
(3, 'Ingeniería de Software', NULL),
(4, 'Bases de datos', NULL),
(5, 'Análisis de algoritmos', NULL),
(6, 'Inteligencia artificial', NULL),
(7, 'Machine Learning', NULL),
(8, 'Robótica', NULL),
(9, 'Modelado y optimización', NULL),
(10, 'Computabilidad y Complejidad', NULL),
(11, 'Redes', NULL),
(12, 'Sistemas Operativos', NULL),
(13, 'Seguridad', NULL),
(14, 'Infraestructura', NULL),
(15, 'Administración de TI', NULL),
(16, 'Desarrollo web', NULL),
(17, 'Desarrollo móvil', NULL),
(18, 'Sistemas en tiempo real', NULL),
(19, 'Administración de proyectos', NULL),
(20, 'Calidad de Software', NULL),
(21, 'Pruebas de Software', NULL),
(22, 'Interacción Humano Computador', NULL),
(23, 'Ingeniería de software empírica', NULL)
)
AS SOURCE ([Id],[Name],[Description]) ON TARGET.[Id] = SOURCE.[Id]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Id],[Name],[Description]) VALUES ([Id],[Name],[Description]);

SET IDENTITY_INSERT [dbo].[ResearchArea] OFF

MERGE INTO [dbo].[Publication] AS Target 
USING (VALUES 
    ('10.1109/CLEI52000.2020.00068','Prueba 1','Many courses in the software engineering area are centered around team-based project development. Evaluating these projects is a challenge due to the difficulty of measuring individual student contributions versus team contributions. The adoption of distributed version control systems like Git enables the measurement of students’ and teams’ contributions to the project. In this work, we analyze the contributions within five software development projects from undergraduate courses that used project-based learning. For this, we generate visualizations of aggregated Git metrics using inequality indexes and inter-decile ratios, which offer insights into the practices and processes followed by students and teams throughout the project development. This approach allowed us to identify both inequality among students’ contributions and development processes with a non-steady pace, rendering a useful feedback tool for instructors and students during the development of the project. Further studies can be conducted to assess the complexity and value of students’ contributions by analyzing their source code commits and other software artifacts.','2020-10-23','Conference','2020 XLVI Latin American Computing Conference (CLEI)',1,'img/picture-default.png',0, null), 
	('10.1109/CLEI52000.2020.00067','Prueba 1','Online programming judges are considered useful and sometimes indispensable tools to support competitive programming, professionals’ recruiting, and programming education. In this last field, the scientific literature on these tools focuses on the learners’ needs, but neglects the requirements of the professors, even though they are who mainly decide whether or not an educational tool is adopted in the courses they teach. This article collected 132 functional requirements for educative online judges, from the scientific literature and programming teachers with experience in the use of this type of tool. To know the degree of support, the requirements were grouped into 27 categories, and a requirements verification was performed with four available educative online judges reported in a recent systematic literature review. A low degree of satisfaction of requirements was found. This result encourages future research to create tools that better support teaching-learning processes and the requirements collected are a useful contribution as a starting point for such research.','2020-10-23','Conference','2020 XLVI Latin American Computing Conference (CLEI)',1,'img/picture-default.png',0, null),
	('10.1110/CLEI52000.2020.00068','Test paged', 'Esto es una prueba', '2020-11-27','Conference','Conferencia Prueba',1,'img/picture-default.png',0, null),
	('10.1111/CLEI52000.2020.00069','Test paged', 'Esto es una prueba 2', '2020-11-29','Conference','Conferencia Prueba 2',1,'img/picture-default.png',0, null),
	('10.1112/CLEI52000.2020.00070','Test paged', 'Esto es una prueba 3', '2020-11-28','Conference','Conferencia Prueba 3',1,'img/picture-default.png',0, null),
	('11/1111','QWERTY', 'Qwerty.', '2011-3-19','Journal','QWERTY', 7, 'img/picture-default.png', 0, null),
	('22/2222','QWERTY', 'Qwerty.', '2014-10-24','Journal','QWERTY', 7, 'img/picture-default.png', 0, null),
	('33/3333','QWERTY', 'Qwerty.', '2015-5-8','Journal','QWERTY', 7, 'img/picture-default.png', 0, null),
	('44/4444','QWERTY', 'Qwerty.', '2017-12-29','Journal','QWERTY', 7, 'img/picture-default.png', 0, null),
	('55/5555','QWERTY', 'Qwerty.', '2021-1-17','Journal','QWERTY', 7, 'img/picture-default.png', 0, null),
	('3096//512','XYLOPHONE', 'Zyxw.', '2022-01-17','Conference','ZYXW.', 512, 'img/picture-default.png', 0, null),
	('3096//513','ZYXW', 'Zyxw.', '2022-01-17','Conference','ZYXW.', 512, 'img/picture-default.png', 0, null),
	('3096//514','ZYXW', 'Xylophone.', '2022-01-17','Conference','ZYXW.', 512, 'img/picture-default.png', 0, null),
	('3096//515','Xy lo pho ne', 'Xy lo pho ne.', '2022-01-17','Conference','Xy lo pho ne.', 512, 'img/picture-default.png', 0, null),
	('XyLOpHonE','ZYXW', 'Zyxw.', '2022-01-17','Conference','ZYXW.', 512, 'img/picture-default.png', 0, null),
	('Reference DOI','Reference summary', 'abcd.', '2022-01-17','Conference','abcd.', 7, 'img/picture-default.png', 0, null),
	('512//:4096','Association.', 'association...', '2014-6-19', 'Conference', 'asso-conference', 52, 'img/DefaultImage.png', 1, null),
	('11.11.11/22.22.22','QWERTY', 'Qwerty.', '2021-1-17','Journal','QWERTY', 7, 'img/picture-default.png', 0, null)
	) 
AS Source ([Id],[Name],[Summary],[Year],[TypePublication],[JournalConference],[ResearchGroupId],[Image],[Deleted],[DocumentPDF]) ON Target.[Id] = Source.[Id] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Id], [Name], [Summary] , [Year] , [TypePublication],[JournalConference],[ResearchGroupId],[Image],[Deleted],[DocumentPDF]) 
VALUES ([Id], [Name], [Summary] , [Year] , [TypePublication],[JournalConference],[ResearchGroupId],[Image],[Deleted],[DocumentPDF]);

--=================InvestigationProject=========================================
MERGE INTO [dbo].[InvestigationProject] AS TARGET
USING 
(VALUES 
(1, N'Intervenciones en infancia temprana para reducir la desigualdad en las oportunidades educativas', N'2019-01-01 00:00:00', N'2021-01-01 00:00:00',1,N'Estudio de intervenciones en infancia temprana para reducir la desigualdad en las oportunidades educativas',N'Estudio de intervenciones en infancia temprana para reducir la desigualdad en las oportunidades educativas',N'img/DefaultImage.png'),
(2, N'Evaluación empírica de la metodología aFPA para la automatización de la medición del tamaño funcional del software', N'2018-01-01 00:00:00', N'2021-01-01 00:00:00',1,N'Evaluar empíricamente tres herramientas prototipo de la metodología aFPA para la automatización de la medición del tamaño funcional del software.',N'Este proyecto tiene como objetivo evaluar empíricamente la metodología para la automatización de la medición del tamaño funcional del software (aFPA). En particular, la metodología de medición aFPA, propuesta como parte de la tesis de doctorado de Cristian Quesada López, se compone de tres procedimientos: medición automática de tamaño funcional, verificación automática de la exactitud de las mediciones de tamaño funcional y evaluación comparativa de modelos de estimación para el aprovechamiento de los resultados de la medición de tamaño funcional.',N'img/DefaultImage.png'),
(3, N'Evaluación de herramientas automatizadas para pruebas de software basadas en modelos', N'2017-01-01 00:00:00', N'2019-01-01 00:00:00',1,N'Caracterizar y evaluar empíricamente herramientas de automatización de pruebas de software basadas en modelos.',N'Este proyecto tiene como objetivo caracterizar y evaluar herramientas automatizadas de pruebas de software basadas en modelos. Las metodologías que se usarán son tomadas de la ingeniería de software empírica, en particular, se hará una revisión sistemática de literatura, un cuasi-experimento y un caso de estudio.',N'img/DefaultImage.png'),
(4, N'Medición automatizada del tamaño funcional de aplicaciones transaccionales', N'2015-01-01 00:00:00', N'2017-01-01 00:00:00',1,N'Automatizar la medición de tamaño funcional de aplicaciones de software transaccionales a partir de su código fuente de acuerdo al estándar IFPUG FPA CPM.',N'Automatizar la medición de tamaño funcional de aplicaciones de software transaccionales a partir de su código fuente de acuerdo al estándar IFPUG FPA CPM.',N'img/DefaultImage.png'),
(5, N'Procedimiento automatizado de medición de contribuciones a partir de repositorios de proyectos de desarrollo de software', N'2021-01-01 00:00:00', N'2023-01-01 00:00:00',1,N'Desarrollar un procedimiento automatizado de medición de contribuciones a partir de repositorios de proyectos de desarrollo de software.',N'La medición de las contribuciones de los(as) desarrolladores(as) durante el desarrollo de un proyecto puede ayudar en la planificación, monitoreo y seguimiento del proyecto, la identificación de riesgos, la administración de la calidad, la coordinación de los equipos, el reconocimiento de miembros de equipo clave por su experticia y compromiso, la retroalimentación constante para la modificación de comportamientos, el incremento en la productividad y la toma de decisiones de negocio informadas.',N'img/DefaultImage.png'),
(6, N'Proyecto sin publicaciones', N'2021-01-01 00:00:00', N'2023-01-01 00:00:00',1,N'Proyecto sin publicaciones descripcíón',N'Proyecto sin publicaciones resumen',N'img/DefaultImage.png')
)
AS SOURCE ([Id],[Name],[StartDate],[EndDate],[InvestigationGroupID],[Description],[Summary],[Image]) ON TARGET.[Id] = SOURCE.[Id]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Id],[Name],[StartDate],[EndDate],[InvestigationGroupID],[Description],[Summary],[Image]) VALUES ([Id],[Name],[StartDate],[EndDate],[InvestigationGroupID],[Description],[Summary],[Image]);

--=================ProjectsImages=========================================
MERGE INTO [dbo].[ProjectImages] AS TARGET
USING 
(VALUES 
(N'img/DefaultImage.png',2)
)
AS SOURCE ([Image],[ProjectId]) ON TARGET.[Image] = SOURCE.[Image] AND TARGET.[ProjectId] = Source.[ProjectId]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Image],[ProjectId]) VALUES ([Image],[ProjectId]);

--=================Theses=========================================
MERGE INTO [dbo].[Thesis] AS TARGET
USING 
(VALUES 
(1, N'Evaluating an automated procedure of machine learning parameter tuning for software effort estimation', N'2020-01-01 00:00:00',N'Evaluating an automated procedure of machine learning parameter tuning for software effort estimation',1,N'img/DefaultImage.png',N'Default',N'Posgrado',N'Villalobos-Arias, L., Quesada-López, C., Martínez, A., & Jenkins, M. (2021). Multi-objective Hyperparameter Tuning for Software Effort Estimation. Proceedings of the XXIII Ibero-American Conference on Software Engineering (CibSE 2021). Costa Rica. Scopus.'),
(2, N'An Empirical Evaluation of a Model-Based Software Testing Tool', N'2018-01-01 00:00:00',N'Model-based testing is one of the most studied approaches by secondary stud-ies  in  the  area  of  software  testing.   Aggregating  knowledge  from  secondary  studies  onmodel-based testing can be useful for both academia and industry.',1,N'img/DefaultImage.png',N'10.19153/cleiej.22.1.3',N'Grado',N'Villalobos-Arias, L., Quesada-López, C., Martinez, A. & Jenkins, M. (2019). “Model-based testingareas, tools and challenges: A tertiary study.” In CLEI Electronic Journal. '),
(3, N'Automatización de la medición del tamaño funcional del software para modelos funcionales obtenidos a partir del análisis dinámico del código fuente', N'2018-01-01 00:00:00',N'Automatización de la medición del tamaño funcional del software para modelos funcionales obtenidos a partir del análisis dinámico del código fuente',1,N'img/DefaultImage.png',N'Default',N'Posgrado',N'Quesada-López, C. & Jenkins, M. (2017). “Procedimientos de medición del tamaño funcional: un mapeo sistemático de literatura.” Proceedings of the XX Ibero-American Conference on Software Engineering (CibSE 2017). Buenos Aires, Argentina'),
(4, N'Evaluación empírica de un protocolo de verificación de exactitud de la medición de tamaño funcional del software ', N'2016-01-01 00:00:00',N'The accuracy of functional size measuring is critical in software project management, because it is one of the key inputs for effort and cost estimation models. ',1,N'img/DefaultImage.png',N'Default',N'Posgrado',N'Quesada-López, C., Madrigal-Sánchez, D., & Jenkins, M. (2020, February). “An Empirical Analysis of IFPUG FPA and COSMIC FFP Measurement Methods”. In International Conference on Information Technology & Systems (pp. 265-274). Springer, Cham. 10.1007/978-3-030-40690-5_26')
)
AS SOURCE ([Id],[Name],[PublicationDate],[Summary],[InvestigationGroupId],[Image],[DOI],[Type],[Reference]) ON TARGET.[Id] = SOURCE.[Id]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Id],[Name],[PublicationDate],[Summary],[InvestigationGroupId],[Image],[DOI],[Type],[Reference]) VALUES ([Id],[Name],[PublicationDate],[Summary],[InvestigationGroupId],[Image],[DOI],[Type],[Reference]);

MERGE INTO [dbo].[PublicationPartOfTesis] AS Target 
USING (VALUES 
    ('10.1109/CLEI52000.2020.00068',1), 
	('10.1109/CLEI52000.2020.00068',2),
	('10.1109/CLEI52000.2020.00068',3),
	('10.1109/CLEI52000.2020.00068',4),
	('10.1109/CLEI52000.2020.00067',1),
	('10.1109/CLEI52000.2020.00067',2),
	('10.1109/CLEI52000.2020.00067',3),
	('10.1109/CLEI52000.2020.00067',4),
	('10.1110/CLEI52000.2020.00068',1),
	('10.1110/CLEI52000.2020.00068',2),
	('10.1110/CLEI52000.2020.00068',3),
	('10.1110/CLEI52000.2020.00068',4),
	('10.1111/CLEI52000.2020.00069',1 ),
	('10.1111/CLEI52000.2020.00069',2 ),
	('10.1111/CLEI52000.2020.00069',3 ),
	('10.1111/CLEI52000.2020.00069',4 ) 
	 ) 
AS Source ([PublicationId], [ThesisId]) ON Target.[PublicationId] = Source.[PublicationId] AND Target.[ThesisId] = Source.[ThesisId] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([PublicationId], [ThesisId]) 
VALUES ([PublicationId], [ThesisId]);

MERGE INTO [dbo].[ThesisPartOfProject] AS TARGET
USING 
(VALUES 
(1,3),
(2,3),
(3,3)
)
AS SOURCE ([InvestigationProjectId],[ThesisId]) ON TARGET.[InvestigationProjectId] = SOURCE.[InvestigationProjectId] and Target.[ThesisId] = Source.[ThesisId]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([InvestigationProjectId],[ThesisId]) VALUES ([InvestigationProjectId],[ThesisId]);



MERGE INTO [dbo].[ProjectAsociatedToPublication] AS Target 
USING (VALUES 
    ('10.1109/CLEI52000.2020.00068',1), 
	('10.1109/CLEI52000.2020.00068',2),
	('10.1109/CLEI52000.2020.00068',3),
	('10.1109/CLEI52000.2020.00068',4),
	('10.1109/CLEI52000.2020.00067',1),
	('10.1109/CLEI52000.2020.00067',2),
	('10.1109/CLEI52000.2020.00067',3),
	('10.1109/CLEI52000.2020.00067',4),
	('10.1110/CLEI52000.2020.00068',1),
	('10.1110/CLEI52000.2020.00068',2),
	('10.1110/CLEI52000.2020.00068',3),
	('10.1110/CLEI52000.2020.00068',4),
	('10.1111/CLEI52000.2020.00069',1 ),
	('10.1111/CLEI52000.2020.00069',2 ),
	('10.1111/CLEI52000.2020.00069',3 ),
	('10.1111/CLEI52000.2020.00069',4 ) 
	 ) 
AS Source ([PublicationId], [InvestigationProjectId]) ON Target.[PublicationId] = Source.[PublicationId] AND Target.[InvestigationProjectId] = Source.[InvestigationProjectId] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([PublicationId], [InvestigationProjectId]) 
VALUES ([PublicationId], [InvestigationProjectId]);


--=================RESEARCH CENTERS=========================================
MERGE INTO [dbo].[ResearchCenter] AS TARGET
USING 
(VALUES 
(1, 'Centro de Investigaciones en Tecnolgías de la Información y Comunicación','El Consejo Universitario creó en junio del 2011 el Centro de Investigaciones en Tecnologías de la Información y Comunicación (CITIC), en su sesión ordinaria N.º 5550. La creación del CITIC se dio con el objetivo de: Producir conocimiento en campos relacionados con computación e informática mediante la promoción, coordinación y desarrollo de la investigación científica inter y transdisciplinaria, y su integración con la acción social y con la docencia en grado y posgrado; la formación y consolidación de grupos de investigación y fungir como observatorio de este campo en el ámbito nacional e internacional. La Vicerrectoría de Investigación, en el oficio VI-3040-2011, del 17 de mayo de 2011, manifiesta que está de acuerdo con la creación del CITIC, y que este reúne los requisitos para ser centro y para ello tomó los siguientes rubros de evaluación.','CITIClogo.png','CITIC')
)
AS SOURCE ([Id],[Name],[Description],[ImageName],[Abbreviation]) ON TARGET.[Id] = SOURCE.[Id]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Id],[Name],[Description],[ImageName],[Abbreviation]) VALUES ([Id],[Name],[Description],[ImageName],[Abbreviation]);

--=================RESEARCH GROUPS=========================================

MERGE INTO [dbo].[ResearchGroup] AS TARGET
USING 
(VALUES 
('Grupo prueba 1','La Ingeniería de Software Empírica enfatiza el uso de estudios empíricos de todo tipo para acumular conocimiento. Los métodos utilizados incluyen experimentos, estudios de casos, encuestas y el uso de los datos disponibles.', NULL ,'2016-04-23',1, 1),
('Grupo prueba 2','La disciplina que estudia cómo las personas interactúan con las computadoras y hasta qué punto las computadoras se desarrollan para interactuar con las personas se llama Interacción Humano-Computadora. HCI consta de tres componentes: los usuarios, los ordenadores y la interacción entre ellos.','img/InteraccionHumanoComputadorLogo.jpg','2015-05-03',1, 0),
('Grupo prueba 3','Nos centramos en el estudio y la investigación de seguridad y privacidad informática con el objetivo de encontrar riesgos potenciales para diseñar y construir tecnologías de protección de datos','img/seguridadYPrivacidadLogo.jpg','2017-03-09',1, 0))
AS SOURCE ([Name],[Description],[ImageName],[CreationDate],[CenterId], [Active]) ON TARGET.[Name] = SOURCE.[Name]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Name],[Description],[ImageName],[CreationDate],[CenterId], [Active]) VALUES ([Name],[Description],[ImageName],[CreationDate],[CenterId], [Active]);

--============================= PEOPLE ====================================

MERGE INTO Person AS Target 
USING (VALUES 
('andrea.alvaradoacon@ucr.ac.cr','Andrea', 'Alvarado', 'Acon'),
('GREIVIN.SANCHEZGARITA@ucr.ac.cr', 'Greivin', 'Sanchez', 'Garita'),
('SEBASTIAN.MONTEROCASTRO@ucr.ac.cr','Sebastian', 'Montero', 'Castro')
)
AS Source ([Email], [FirstName], [FirstLastName],[SecondLastName]) ON Target.[Email] = Source.[Email] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Email], [FirstName], [FirstLastName],[SecondLastName]) VALUES ([Email], [FirstName], [FirstLastName],[SecondLastName]);

MERGE INTO Collaborator AS Target 
USING (VALUES 
('andrea.alvaradoacon@ucr.ac.cr', 'Investigadora Principal'),
('GREIVIN.SANCHEZGARITA@ucr.ac.cr', 'Investigador Secundario'),
('SEBASTIAN.MONTEROCASTRO@ucr.ac.cr', 'Investigador Secundario')
)
AS Source ([Email], [Role]) ON Target.[Email] = Source.[Email] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Email], [Role]) VALUES ([Email], [Role]);

MERGE INTO Investigator AS Target 
USING (VALUES 
('andrea.alvaradoacon@ucr.ac.cr'),
('GREIVIN.SANCHEZGARITA@ucr.ac.cr'),
('SEBASTIAN.MONTEROCASTRO@ucr.ac.cr')
)
AS Source ([Email]) ON Target.[Email] = Source.[Email] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Email]) VALUES ([Email]);

MERGE INTO CollaboratorPartOfGroup AS Target
USING(VALUES
('andrea.alvaradoacon@ucr.ac.cr',1,'2015-01-01', '2018-08-08'),
('andrea.alvaradoacon@ucr.ac.cr',2,'2015-01-01', '2018-08-08'),
('GREIVIN.SANCHEZGARITA@ucr.ac.cr',3,'2015-01-01', '2018-08-08'),
('SEBASTIAN.MONTEROCASTRO@ucr.ac.cr',1,'2015-01-01', '2018-08-08'),
('SEBASTIAN.MONTEROCASTRO@ucr.ac.cr',3,'2015-01-01', '2018-08-08')
)
AS Source ([CollaboratorEmail], [InvestigationGroupId], [StartDate],[EndDate]) ON Target.[CollaboratorEmail] = Source.[CollaboratorEmail] and Target.[InvestigationGroupId] = Source.[InvestigationGroupId]
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([CollaboratorEmail], [InvestigationGroupId], [StartDate],[EndDate]) VALUES ([CollaboratorEmail], [InvestigationGroupId], [StartDate],[EndDate]);

MERGE INTO AcademicProfile AS Target 
USING (VALUES 
('andrea.alvaradoacon@ucr.ac.cr', 'Biography', './img/ProfilePictures/andreaalvarado.png',null,'https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
('GREIVIN.SANCHEZGARITA@ucr.ac.cr', 'Biography', './img/ProfilePictures/greivinsanchez.png',null,null, 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
('SEBASTIAN.MONTEROCASTRO@ucr.ac.cr', 'Biography', './img/ProfilePictures/sebastianmontero.png',null,'https://www.facebook.com/', null, 'https://www.linkedin.com/', 'N/A')
)
AS Source ([Email], [Biography], [ProfilePic],[Degree], [FacebookLink], [GitHubLink], [LinkedInLink], [Title]) ON Target.[Email] = Source.[Email] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Email], [Biography], [ProfilePic],[Degree], [FacebookLink], [GitHubLink], [LinkedInLink], [Title]) VALUES ([Email], [Biography], [ProfilePic],[Degree], [FacebookLink], [GitHubLink], [LinkedInLink], [Title]);

MERGE INTO AcademicUnit AS Target 
USING (VALUES 
('Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
('Centro de Ciencias de la Computación')
)
AS Source ([Name]) ON Target.[Name] = Source.[Name] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Name]) VALUES ([Name]);

MERGE INTO PersonWorksForUnit AS Target 
USING (VALUES 
('andrea.alvaradoacon@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
('andrea.alvaradoacon@ucr.ac.cr','Centro de Ciencias de la Computación')
)
AS Source ([Email],[UnitName]) ON Target.[Email] = Source.[Email] and Target.[UnitName] = Source.[UnitName]
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Email],[UnitName]) VALUES ([Email],[UnitName]);

MERGE INTO [dbo].[University] AS Target 
USING (VALUES 
    ('Universidad de Costa Rica'),
    ('Instituto Tecnológico de Costa Rica')
)
AS Source ([Name]) ON Target.[Name] = Source.[Name] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Name]) 
VALUES ([Name]);

MERGE INTO [dbo].[PersonsBelongsToUniversity] AS Target 
USING (VALUES 
('andrea.alvaradoacon@ucr.ac.cr','Universidad de Costa Rica'),
('andrea.alvaradoacon@ucr.ac.cr','Instituto Tecnológico de Costa Rica')
)
AS Source ([PersonEmail], [UniversityName]) ON Target.[PersonEmail] = Source.[PersonEmail] AND Target.[UniversityName] = Source.[UniversityName] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([PersonEmail], [UniversityName]) 
VALUES ([PersonEmail], [UniversityName]);

--=================RESEARCH CENTERS=========================================
MERGE INTO [dbo].[ResearchCenter] AS TARGET
USING 
(VALUES 
(1, 'Centro de Investigaciones en Tecnolgías de la Información y Comunicación','El Consejo Universitario creó en junio del 2011 el Centro de Investigaciones en Tecnologías de la Información y Comunicación (CITIC), en su sesión ordinaria N.º 5550. La creación del CITIC se dio con el objetivo de: Producir conocimiento en campos relacionados con computación e informática mediante la promoción, coordinación y desarrollo de la investigación científica inter y transdisciplinaria, y su integración con la acción social y con la docencia en grado y posgrado; la formación y consolidación de grupos de investigación y fungir como observatorio de este campo en el ámbito nacional e internacional. La Vicerrectoría de Investigación, en el oficio VI-3040-2011, del 17 de mayo de 2011, manifiesta que está de acuerdo con la creación del CITIC, y que este reúne los requisitos para ser centro y para ello tomó los siguientes rubros de evaluación.','CITIClogo.png','CITIC')
)
AS SOURCE ([Id],[Name],[Description],[ImageName],[Abbreviation]) ON TARGET.[Id] = SOURCE.[Id]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Id],[Name],[Description],[ImageName],[Abbreviation]) VALUES ([Id],[Name],[Description],[ImageName],[Abbreviation]);

--=================RESEARCH GROUPS=========================================

MERGE INTO [dbo].[ResearchGroup] AS TARGET
USING 
(VALUES 
('Grupo prueba 1','La Ingeniería de Software Empírica enfatiza el uso de estudios empíricos de todo tipo para acumular conocimiento. Los métodos utilizados incluyen experimentos, estudios de casos, encuestas y el uso de los datos disponibles.', NULL ,'2016-04-23',1, 1),
('Grupo prueba 2','La disciplina que estudia cómo las personas interactúan con las computadoras y hasta qué punto las computadoras se desarrollan para interactuar con las personas se llama Interacción Humano-Computadora. HCI consta de tres componentes: los usuarios, los ordenadores y la interacción entre ellos.','img/InteraccionHumanoComputadorLogo.jpg','2015-05-03',1, 0),
('Grupo prueba 3','Nos centramos en el estudio y la investigación de seguridad y privacidad informática con el objetivo de encontrar riesgos potenciales para diseñar y construir tecnologías de protección de datos','img/seguridadYPrivacidadLogo.jpg','2017-03-09',1, 0))
AS SOURCE ([Name],[Description],[ImageName],[CreationDate],[CenterId], [Active]) ON TARGET.[Name] = SOURCE.[Name]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Name],[Description],[ImageName],[CreationDate],[CenterId], [Active]) VALUES ([Name],[Description],[ImageName],[CreationDate],[CenterId], [Active]);


--=================RESEARCH NEWS=========================================

MERGE INTO [dbo].[News] AS TARGET
USING 
(VALUES 
( N'Noticia prueba 1',NULL, N'2021-07-30 00:00:00',N'2021-07-30 00:00:00',N'2021-07-30 00:00:00',N'Descripción prueba 1',1),
( N'Noticia prueba 2',NULL, N'2021-07-30 00:00:00',N'2021-07-30 00:00:00',N'2021-07-30 00:00:00',N'Descripción prueba 2',1),
( N'Noticia prueba 3',NULL, N'2021-07-30 00:00:00',N'2021-07-30 00:00:00',N'2021-07-30 00:00:00',N'Descripción prueba 3',1),
( N'Noticia prueba 4',NULL, N'2021-07-30 00:00:00',N'2021-07-30 00:00:00',N'2021-07-30 00:00:00',N'Descripción prueba 4',2),
( N'Noticia prueba 5',NULL, N'2021-07-30 00:00:00',N'2021-07-30 00:00:00',N'2021-07-30 00:00:00',N'Descripción prueba 5',2),
( N'Noticia prueba 6',NULL, N'2021-07-30 00:00:00',N'2021-07-30 00:00:00',N'2021-07-30 00:00:00',N'Descripción prueba 6',2),
( N'Noticia prueba 7',NULL, N'2021-07-30 00:00:00',N'2021-07-30 00:00:00',N'2021-07-30 00:00:00',N'Descripción prueba 7',3),
( N'Noticia prueba term 8',NULL, N'2021-07-30 00:00:00',N'2021-07-30 00:00:00',N'2021-07-30 00:00:00',N'Descripción prueba 8',3),
( N'Noticia prueba term 9',NULL, N'2021-07-30 00:00:00',N'2021-07-30 00:00:00',N'2021-07-30 00:00:00',N'Descripción prueba 9',3))
AS SOURCE ([Title],[MainImageId],[PublicationDate],[CreationDate],[EndDate],[Description],[GroupId]) ON TARGET.[Title] = SOURCE.[Title]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Title],[MainImageId],[PublicationDate],[CreationDate],[EndDate],[Description],[GroupId]) VALUES ([Title],[MainImageId],[PublicationDate],[CreationDate],[EndDate],[Description],[GroupId]);

MERGE INTO [dbo].[NewsImage] AS TARGET
USING 
(VALUES
    ('img/news/1.png',1),
    ('img/news/2.jpg',2),
    ('img/news/3.png',3),
    ('img/news/4.png',4),
    ('img/news/5.png',5),
    ('img/news/6.jpg',6),
    ('img/news/7.jpg',7),
    ('img/news/8.jpeg',8),
    ('img/news/9.png',9)
)
AS SOURCE ([Path],[NewsID]) ON TARGET.[Path] = SOURCE.[Path]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Path],[NewsID]) VALUES ([Path],[NewsID]);

MERGE INTO ReferenceListPublication AS Target 
USING (VALUES 
		
	('11.11.11/22.22.22','Referencia 1',1),
	('11.11.11/22.22.22','Referencia 2',2),
	('11.11.11/22.22.22','Referencia 3',3),
	('11.11.11/22.22.22','Referencia 4',4)

	)
AS Source ([IdPublication], [Reference], [Order]) ON Target.[IdPublication] = Source.[IdPublication] AND Target.[Reference] = Source.[Reference] AND Target.[Order] = Source.[Order]
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([IdPublication], [Reference], [Order] ) 
VALUES ([IdPublication], [Reference], [Order] );

EXEC SP_AddMainImageToNews @image = 1, @news = 1;
EXEC SP_AddMainImageToNews @image = 2, @news = 2;
EXEC SP_AddMainImageToNews @image = 3, @news = 3;
EXEC SP_AddMainImageToNews @image = 4, @news = 4;
EXEC SP_AddMainImageToNews @image = 5, @news = 5;
EXEC SP_AddMainImageToNews @image = 6, @news = 6;
EXEC SP_AddMainImageToNews @image = 7, @news = 7;
EXEC SP_AddMainImageToNews @image = 8, @news = 8;
EXEC SP_AddMainImageToNews @image = 9, @news = 9;

MERGE INTO [dbo].[Contact] AS TARGET
USING
(VALUES
	('Pedro Segura',N'2021-07-30 00:00:00',N'2021-12-30 00:00:00','pedricola@gmail.com',NULL,1,1,0),
	('Pedro Segura2',N'2021-07-30 00:00:00',N'2021-12-30 00:00:00','pedricola2@gmail.com',NULL,0,1,0),
	('Pedro Segura3',N'2021-07-30 00:00:00',N'2021-12-30 00:00:00','pedricola3@gmail.com',NULL,0,1,0)
)
AS SOURCE ([Name],[StartDate],[EndDate],[Email],[Telephone],[MainContact],[GroupId],[Deleted]) ON TARGET .[Name] = SOURCE.[Name]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Name],[StartDate],[EndDate],[Email],[Telephone],[MainContact],[GroupId],[Deleted]) VALUES ([Name],[StartDate],[EndDate],[Email],[Telephone],[MainContact],[GroupId],[Deleted]);


MERGE INTO [dbo].[WorkWithUs] AS TARGET
USING 
(VALUES
    ('Nombre','Descripcion','Image','correo@ucr.ac.cr','Pre','Pregrado','Posgrado','Voluntario')
   
)
AS SOURCE ([Name],[Description],[ImageName],[Email],[PreRequisites],[Pregrado],[Posgrado],[Voluntario]) ON TARGET.[Name] = SOURCE.[Name]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Name],[Description],[ImageName],[Email],[PreRequisites],[Pregrado],[Posgrado],[Voluntario]) VALUES ([Name],[Description],[ImageName],[Email],[PreRequisites],[Pregrado],[Posgrado],[Voluntario]);

