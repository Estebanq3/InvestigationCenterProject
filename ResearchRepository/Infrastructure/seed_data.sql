
use Database_ResearchRepositoryTest;

DELETE FROM PublicationPartOfTesis;
DELETE FROM ProjectAsociatedToPublication;
DELETE FROM ReferenceListPublication;
DELETE FROM Publication;
DELETE FROM InvestigationProject;
DELETE FROM Thesis;


MERGE INTO [dbo].[Publication] AS Target 
USING (VALUES 
    ('10.1109/CLEI52000.2020.00068','Prueba 1','Many courses in the software engineering area are centered around team-based project development. Evaluating these projects is a challenge due to the difficulty of measuring individual student contributions versus team contributions. The adoption of distributed version control systems like Git enables the measurement of students’ and teams’ contributions to the project. In this work, we analyze the contributions within five software development projects from undergraduate courses that used project-based learning. For this, we generate visualizations of aggregated Git metrics using inequality indexes and inter-decile ratios, which offer insights into the practices and processes followed by students and teams throughout the project development. This approach allowed us to identify both inequality among students’ contributions and development processes with a non-steady pace, rendering a useful feedback tool for instructors and students during the development of the project. Further studies can be conducted to assess the complexity and value of students’ contributions by analyzing their source code commits and other software artifacts.','2020-10-23','Conference','2020 XLVI Latin American Computing Conference (CLEI)',1,'img/picture-default.png',0), 
	('10.1109/CLEI52000.2020.00067','Prueba 1','Online programming judges are considered useful and sometimes indispensable tools to support competitive programming, professionals’ recruiting, and programming education. In this last field, the scientific literature on these tools focuses on the learners’ needs, but neglects the requirements of the professors, even though they are who mainly decide whether or not an educational tool is adopted in the courses they teach. This article collected 132 functional requirements for educative online judges, from the scientific literature and programming teachers with experience in the use of this type of tool. To know the degree of support, the requirements were grouped into 27 categories, and a requirements verification was performed with four available educative online judges reported in a recent systematic literature review. A low degree of satisfaction of requirements was found. This result encourages future research to create tools that better support teaching-learning processes and the requirements collected are a useful contribution as a starting point for such research.','2020-10-23','Conference','2020 XLVI Latin American Computing Conference (CLEI)',1,'img/picture-default.png',0),
	('10.1110/CLEI52000.2020.00068','Test paged', 'Esto es una prueba', '2020-11-27','Conference','Conferencia Prueba',1,'img/picture-default.png',0),
	('10.1111/CLEI52000.2020.00069','Test paged', 'Esto es una prueba 2', '2020-11-29','Conference','Conferencia Prueba 2',1,'img/picture-default.png',0),
	('10.1112/CLEI52000.2020.00070','Test paged', 'Esto es una prueba 3', '2020-11-28','Conference','Conferencia Prueba 3',1,'img/picture-default.png',0)	 
	) 
AS Source ([Id],[Name],[Summary],[Year],[TypePublication],[JournalConference],[ResearchGroupId],[Image],[Deleted]) ON Target.[Id] = Source.[Id] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Id], [Name], [Summary] , [Year] , [TypePublication],[JournalConference],[ResearchGroupId],[Image],[Deleted]) 
VALUES ([Id], [Name], [Summary] , [Year] , [TypePublication],[JournalConference],[ResearchGroupId],[Image],[Deleted]);

MERGE INTO [dbo].[InvestigationProject] AS TARGET
USING 
(VALUES 
(1, N'Intervenciones en infancia temprana para reducir la desigualdad en las oportunidades educativas', N'2019-01-01 00:00:00', N'2021-01-01 00:00:00',1,N'Estudio de intervenciones en infancia temprana para reducir la desigualdad en las oportunidades educativas',N'Estudio de intervenciones en infancia temprana para reducir la desigualdad en las oportunidades educativas',N'img/DefaultImage.png'),
(2, N'Evaluación empírica de la metodología aFPA para la automatización de la medición del tamaño funcional del software', N'2018-01-01 00:00:00', N'2021-01-01 00:00:00',1,N'Evaluar empíricamente tres herramientas prototipo de la metodología aFPA para la automatización de la medición del tamaño funcional del software.',N'Este proyecto tiene como objetivo evaluar empíricamente la metodología para la automatización de la medición del tamaño funcional del software (aFPA). En particular, la metodología de medición aFPA, propuesta como parte de la tesis de doctorado de Cristian Quesada López, se compone de tres procedimientos: medición automática de tamaño funcional, verificación automática de la exactitud de las mediciones de tamaño funcional y evaluación comparativa de modelos de estimación para el aprovechamiento de los resultados de la medición de tamaño funcional.',N'img/DefaultImage.png'),
(3, N'Evaluación de herramientas automatizadas para pruebas de software basadas en modelos', N'2017-01-01 00:00:00', N'2019-01-01 00:00:00',1,N'Caracterizar y evaluar empíricamente herramientas de automatización de pruebas de software basadas en modelos.',N'Este proyecto tiene como objetivo caracterizar y evaluar herramientas automatizadas de pruebas de software basadas en modelos. Las metodologías que se usarán son tomadas de la ingeniería de software empírica, en particular, se hará una revisión sistemática de literatura, un cuasi-experimento y un caso de estudio.',N'img/DefaultImage.png'),
(4, N'Medición automatizada del tamaño funcional de aplicaciones transaccionales', N'2015-01-01 00:00:00', N'2017-01-01 00:00:00',1,N'Automatizar la medición de tamaño funcional de aplicaciones de software transaccionales a partir de su código fuente de acuerdo al estándar IFPUG FPA CPM.',N'Automatizar la medición de tamaño funcional de aplicaciones de software transaccionales a partir de su código fuente de acuerdo al estándar IFPUG FPA CPM.',N'img/DefaultImage.png'),
(5, N'Procedimiento automatizado de medición de contribuciones a partir de repositorios de proyectos de desarrollo de software', N'2021-01-01 00:00:00', N'2023-01-01 00:00:00',1,N'Desarrollar un procedimiento automatizado de medición de contribuciones a partir de repositorios de proyectos de desarrollo de software.',N'La medición de las contribuciones de los(as) desarrolladores(as) durante el desarrollo de un proyecto puede ayudar en la planificación, monitoreo y seguimiento del proyecto, la identificación de riesgos, la administración de la calidad, la coordinación de los equipos, el reconocimiento de miembros de equipo clave por su experticia y compromiso, la retroalimentación constante para la modificación de comportamientos, el incremento en la productividad y la toma de decisiones de negocio informadas.',N'img/DefaultImage.png')
)

AS SOURCE ([Id],[Name],[StartDate],[EndDate],[InvestigationGroupID],[Description],[Summary],[Image]) ON TARGET.[Id] = SOURCE.[Id]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Id],[Name],[StartDate],[EndDate],[InvestigationGroupID],[Description],[Summary],[Image]) VALUES ([Id],[Name],[StartDate],[EndDate],[InvestigationGroupID],[Description],[Summary],[Image]);
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
