/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

MERGE INTO [dbo].[ResearchCenter] AS TARGET
USING 
(VALUES 
(1, 'Centro de Investigaciones en Tecnologías de la Información y Comunicación','El Consejo Universitario creó en junio del 2011 el Centro de Investigaciones en Tecnologías de la Información y Comunicación (CITIC), en su sesión ordinaria N.º 5550. La creación del CITIC se dio con el objetivo de: Producir conocimiento en campos relacionados con computación e informática mediante la promoción, coordinación y desarrollo de la investigación científica inter y transdisciplinaria, y su integración con la acción social y con la docencia en grado y posgrado; la formación y consolidación de grupos de investigación y fungir como observatorio de este campo en el ámbito nacional e internacional. La Vicerrectoría de Investigación, en el oficio VI-3040-2011, del 17 de mayo de 2011, manifiesta que está de acuerdo con la creación del CITIC, y que este reúne los requisitos para ser centro y para ello tomó los siguientes rubros de evaluación.','CITIClogo.png','CITIC')
)
AS SOURCE ([Id],[Name],[Description],[ImageName],[Abbreviation]) ON TARGET.[Id] = SOURCE.[Id]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Id],[Name],[Description],[ImageName],[Abbreviation]) VALUES ([Id],[Name],[Description],[ImageName],[Abbreviation]);

MERGE INTO [dbo].[ResearchGroup] AS TARGET
USING 
(VALUES 
('Ingeniería de Software Empírica','La Ingeniería de Software Empírica enfatiza el uso de estudios empíricos de todo tipo para acumular conocimiento. Los métodos utilizados incluyen experimentos, estudios de casos, encuestas y el uso de los datos disponibles.','img/ingenieriaSoftwareEmpiricaLogo.png','2016-04-23',1, 1),
('Interacción Humano Computador','La disciplina que estudia cómo las personas interactúan con las computadoras y hasta qué punto las computadoras se desarrollan para interactuar con las personas se llama Interacción Humano-Computadora. HCI consta de tres componentes: los usuarios, los ordenadores y la interacción entre ellos.','img/InteraccionHumanoComputadorLogo.jpg','2015-05-03',1, 0),
('Seguridad y Privacidad','Nos centramos en el estudio y la investigación de seguridad y privacidad informática con el objetivo de encontrar riesgos potenciales para diseñar y construir tecnologías de protección de datos','img/seguridadYPrivacidadLogo.jpg','2017-03-09',1, 0))
AS SOURCE ([Name],[Description],[ImageName],[CreationDate],[CenterId], [Active]) ON TARGET.[Name] = SOURCE.[Name]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Name],[Description],[ImageName],[CreationDate],[CenterId], [Active]) VALUES ([Name],[Description],[ImageName],[CreationDate],[CenterId], [Active]);

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

SET IDENTITY_INSERT [dbo].[ResearchArea] OFF;

MERGE INTO [dbo].[ResearchAreaResearchSubArea] AS TARGET
USING
(VALUES
(4, 1),
(4, 2),
(4, 3),
(5, 1),
(6, 1),
(7, 1),
(8, 1),
(9, 1),
(9, 2),
(10, 1),
(11, 2),
(12, 2),
(13, 2),
(14, 2),
(15, 2),
(16, 3),
(17, 3),
(18, 3),
(19, 3),
(20, 3),
(21, 3),
(22, 3),
(23, 3)
)
AS SOURCE ([ResearchSubAreasId],[ResearchAreasId]) ON TARGET.[ResearchSubAreasId] = SOURCE.[ResearchSubAreasId] AND TARGET.[ResearchAreasId] = SOURCE.[ResearchAreasId]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([ResearchSubAreasId],[ResearchAreasId]) VALUES ([ResearchSubAreasId],[ResearchAreasId]);


MERGE INTO [dbo].[InvestigationProject] AS TARGET
USING 
(VALUES 
(N'Intervenciones en infancia temprana para reducir la desigualdad en las oportunidades educativas', N'2019-01-01 00:00:00', N'2021-01-01 00:00:00',1,N'Estudio de intervenciones en infancia temprana para reducir la desigualdad en las oportunidades educativas',N'Estudio de intervenciones en infancia temprana para reducir la desigualdad en las oportunidades educativas',N'img/DefaultImage.png'),
(N'Evaluación empírica de la metodología aFPA para la automatización de la medición del tamaño funcional del software', N'2018-01-01 00:00:00', N'2021-01-01 00:00:00',1,N'Evaluar empíricamente tres herramientas prototipo de la metodología aFPA para la automatización de la medición del tamaño funcional del software.',N'Este proyecto tiene como objetivo evaluar empíricamente la metodología para la automatización de la medición del tamaño funcional del software (aFPA). En particular, la metodología de medición aFPA, propuesta como parte de la tesis de doctorado de Cristian Quesada López, se compone de tres procedimientos: medición automática de tamaño funcional, verificación automática de la exactitud de las mediciones de tamaño funcional y evaluación comparativa de modelos de estimación para el aprovechamiento de los resultados de la medición de tamaño funcional.',N'img/DefaultImage.png'),
(N'Evaluación de herramientas automatizadas para pruebas de software basadas en modelos', N'2017-01-01 00:00:00', N'2019-01-01 00:00:00',1,N'Caracterizar y evaluar empíricamente herramientas de automatización de pruebas de software basadas en modelos.',N'Este proyecto tiene como objetivo caracterizar y evaluar herramientas automatizadas de pruebas de software basadas en modelos. Las metodologías que se usarán son tomadas de la ingeniería de software empírica, en particular, se hará una revisión sistemática de literatura, un cuasi-experimento y un caso de estudio.',N'img/DefaultImage.png'),
(N'Medición automatizada del tamaño funcional de aplicaciones transaccionales', N'2015-01-01 00:00:00', N'2017-01-01 00:00:00',1,N'Automatizar la medición de tamaño funcional de aplicaciones de software transaccionales a partir de su código fuente de acuerdo al estándar IFPUG FPA CPM.',N'Automatizar la medición de tamaño funcional de aplicaciones de software transaccionales a partir de su código fuente de acuerdo al estándar IFPUG FPA CPM.',N'img/DefaultImage.png'),
(N'Procedimiento automatizado de medición de contribuciones a partir de repositorios de proyectos de desarrollo de software', N'2021-01-01 00:00:00', N'2023-01-01 00:00:00',1,N'Desarrollar un procedimiento automatizado de medición de contribuciones a partir de repositorios de proyectos de desarrollo de software.',N'La medición de las contribuciones de los(as) desarrolladores(as) durante el desarrollo de un proyecto puede ayudar en la planificación, monitoreo y seguimiento del proyecto, la identificación de riesgos, la administración de la calidad, la coordinación de los equipos, el reconocimiento de miembros de equipo clave por su experticia y compromiso, la retroalimentación constante para la modificación de comportamientos, el incremento en la productividad y la toma de decisiones de negocio informadas.',N'img/DefaultImage.png')
)

AS SOURCE ([Name],[StartDate],[EndDate],[InvestigationGroupID],[Description],[Summary],[Image]) ON TARGET.[Name] = SOURCE.[Name]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Name],[StartDate],[EndDate],[InvestigationGroupID],[Description],[Summary],[Image]) VALUES ([Name],[StartDate],[EndDate],[InvestigationGroupID],[Description],[Summary],[Image]);

MERGE INTO [dbo].[Thesis] AS TARGET
USING 
(VALUES 
(N'Evaluating an automated procedure of machine learning parameter tuning for software effort estimation', N'2020-01-01 00:00:00',N'Evaluating an automated procedure of machine learning parameter tuning for software effort estimation',1,N'img/DefaultImage.png',N'Default',N'Posgrado',N'Villalobos-Arias, L., Quesada-López, C., Martínez, A., & Jenkins, M. (2021). Multi-objective Hyperparameter Tuning for Software Effort Estimation. Proceedings of the XXIII Ibero-American Conference on Software Engineering (CibSE 2021). Costa Rica. Scopus.'),
(N'An Empirical Evaluation of a Model-Based Software Testing Tool', N'2018-01-01 00:00:00',N'Model-based testing is one of the most studied approaches by secondary stud-ies  in  the  area  of  software  testing.   Aggregating  knowledge  from  secondary  studies  onmodel-based testing can be useful for both academia and industry.',1,N'img/DefaultImage.png',N'10.19153/cleiej.22.1.3',N'Grado',N'Villalobos-Arias, L., Quesada-López, C., Martinez, A. & Jenkins, M. (2019). “Model-based testingareas, tools and challenges: A tertiary study.” In CLEI Electronic Journal. '),
(N'Automatización de la medición del tamaño funcional del software para modelos funcionales obtenidos a partir del análisis dinámico del código fuente', N'2018-01-01 00:00:00',N'Automatización de la medición del tamaño funcional del software para modelos funcionales obtenidos a partir del análisis dinámico del código fuente',1,N'img/DefaultImage.png',N'Default',N'Posgrado',N'Quesada-López, C. & Jenkins, M. (2017). “Procedimientos de medición del tamaño funcional: un mapeo sistemático de literatura.” Proceedings of the XX Ibero-American Conference on Software Engineering (CibSE 2017). Buenos Aires, Argentina'),
(N'Evaluación empírica de un protocolo de verificación de exactitud de la medición de tamaño funcional del software ', N'2016-01-01 00:00:00',N'The accuracy of functional size measuring is critical in software project management, because it is one of the key inputs for effort and cost estimation models. ',1,N'img/DefaultImage.png',N'Default',N'Posgrado',N'Quesada-López, C., Madrigal-Sánchez, D., & Jenkins, M. (2020, February). “An Empirical Analysis of IFPUG FPA and COSMIC FFP Measurement Methods”. In International Conference on Information Technology & Systems (pp. 265-274). Springer, Cham. 10.1007/978-3-030-40690-5_26'),
(N'Medición del tamaño funcional en el desarrollo de software dirigido por modelos', N'2017-01-01 00:00:00',N'Automating functional size measurement (FSM) for software applications that use specific development frameworks is a challenge for the industry.',1,N'img/DefaultImage.png',N'Default',N'Posgrado',N'Quesada-López, C., Martínez, A., Jenkins, M., Salas, L. C., & Gómez, J. C. (2019, November). “Automated Functional Size Measurement: A Multiple Case Study in the Industry”. In International Conference on Product-Focused Software Process Improvement (pp. 263-279). Springer, Cham. 10.1007/978-3-030-35333-9_19'),
(N'Mining software repositories to automatically measure developer code contributions Maestria academica ', N'2021-01-01 00:00:00',N'Mining software repositories to automatically measure developer code contributions Maestria academica',1,N'img/DefaultImage.png',N'Default',N'Posgrado',N'Hamer, S., Quesada-López, C., Martínez, A., Jenkins, M. (2021). Students projects source code changes impact on software quality through static analysis. 14th International Conference on the Quality of Information and Communications Technology. Springer.'),
(N'Validación empírica del uso de plantillas para la identificación de requerimientos de seguridad de aplicaciones de software', N'2016-01-01 00:00:00',N'10.23919/CISTI.2019.8760631',1,N'img/DefaultImage.png',N'Default',N'Posgrado',N'Martínez, A., Quesada-López, C. & Jenkins, M. (2019, June). “Identifying implied security requirements from functional requirements”. In 2019 14th Iberian Conference on InformationSystems and Technologies (CISTI). IEEE. 10.23919/CISTI.2019.8760631')
)
AS SOURCE ([Name],[PublicationDate],[Summary],[InvestigationGroupId],[Image],[DOI],[Type],[Reference]) ON TARGET.[Name] = SOURCE.[Name]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Name],[PublicationDate],[Summary],[InvestigationGroupId],[Image],[DOI],[Type],[Reference]) VALUES ([Name],[PublicationDate],[Summary],[InvestigationGroupId],[Image],[DOI],[Type],[Reference]);


MERGE INTO [dbo].[News] AS TARGET
USING 
(VALUES 
( N'El Dr. Mauricio Soto imparte charla "Optimizando la calidad de parches de código creados en el proceso de reparación automática de software',NULL, N'2021-10-21 00:00:00',N'2021-10-21 00:00:00',N'2021-10-21 00:00:00',N'El pasado 20 de octubre del 2021, el Dr. Mauricio Soto impartió una charla con el título&nbsp;"Optimizando la calidad de parches de código creados en el proceso de reparación automática de softwares". La exposición exploró rigurosamente este fenómeno en programas del mundo real y describe un conjunto de mecanismos para mejorar los componentes clave del proceso de reparación automática de programas para generar parches de mayor calidad. Estos mecanismos incluyen un análisis del comportamiento de las test suites y sus características clave para la reparación automática del programa, el análisis de cambios en el código hecho por los desarrolladores para informar la distribución de la selección de operadores de mutación, y el impacto que tiene la diversidad de parches en el proceso de reparación automática que conduce a un aumento en la calidad de los parches plausibles generados. El video de la charla se encuentra en el siguiente enlace: <a href="https://www.youtube.com/watch?v=D8Z42JCMGEk" target="_blank">https://www.youtube.com/watch?v=D8Z42JCMGEk</a>',1),
( N'RobIE++: El robot que enseñará programación a más de 68 mil niños de preescolar (TITIBOTS y TITIBOTS Colab)',NULL, N'2021-08-23 00:00:00',N'2021-08-23 00:00:00',N'2021-08-23 00:00:00',N'<p align="justify">Alrededor de 68.500 estudiantes de preescolar de 909 escuelas, iniciarán su aprendizaje en el mundo de la programación de la mano de RobIE++, un pequeño robot que hará más atractivas y lúdicas sus lecciones.</p>
<p align="justify">RobIE++ es un prototipo móvil diseñado por la Fundación Omar Dengo como complemento a la propuesta didáctica LIE++ para preescolar y su objetivo es enseñar programación (coding), a los niños en edades en las que se encuentran más abiertos y curiosos al mundo que les rodea.</p>
<p align="justify">Cada centro educativo recibirá entre 1 y 2 robots y de 1 a 6 tabletas, de acuerdo con la población de preescolar que atienden. En total, se entregarán 1.442 ejemplares de RobIE++, además de 4,144 tabletas que se utilizarán para que los niños construyan el programa que le dictará al robot las instrucciones a seguir.</p>
<p align="justify">RobIE++ utiliza Titibot y Titibot Colab, un software diseñado exclusivamente para preescolares por la doctora <strong>Kryscia Ramírez</strong>, de la <strong>Universidad de Costa Rica</strong>; y cuya licencia (no exclusiva) fue otorgada a la FOD como herramienta para el aprendizaje, con base en el convenio de colaboración entre ambas instituciones.</p>',1),
( N'Investigador del CITIC participó en la Conferencia Internacional sobre Tendencias Inteligentes en Sistemas, Seguridad y Sostenibilidad (WS4 2021)',NULL, N'2021-07-30 00:00:00',N'2021-07-30 00:00:00',N'2021-07-30 00:00:00',N'El investigador Mag. Jose Antonio Brenes Carranza participó en la Conferencia Internacional sobre Tendencias Inteligentes en Sistemas, Seguridad y Sostenibilidad (WS4 2021), la cual se llevó a cabo virtualmente del 29 al 30 de julio, por medio de la plataforma Zoom. El objetivo de esta conferencia internacional es brindar una oportunidad para que los investigadores, académicos, personas de la industria y estudiantes interactúen e intercambien ideas, experiencias y conocimientos en el Tendencias y estrategias actuales para las Tecnologías de la Información y la Comunicación. ',1),
( N'Celebración del aniversario ECCI CITIC PCI',NULL, N'2021-06-28 00:00:00',N'2021-06-28 00:00:00',N'2021-06-28 00:00:00',N'El pasado miércoles&nbsp;23&nbsp;de junio&nbsp;del 2021, se realizó la celebración del aniversario de la Escuela de Ciencias de la Computación e Informática (ECCI), Programa de Posgrado en Computación e Informática (PCI) y Centro de Investigaciones en Tecnologías de la Información y Comunicación (CITIC). La actividad fue desarrollada por medio de la plataforma Zoom y conto con la participación del Dr. Carlos Castro, <em>Software Engineer</em> en Google.&nbsp;',1),
( N'El Director del CITIC participa en el taller de elaboración del Plan Nacional de Desarrollo de las Telecomunicaciones 2022-2027',NULL, N'2021-05-19 00:00:00',N'2021-05-19 00:00:00',N'2021-05-19 00:00:00',N'<p>El Prof. Marcelo Jenkins partició en el taller de elaboración del Plan Nacional de Desarrollo de las Telecomunicaciones 2022-2027, actividad coordinada por el Ministerio de Ciencia, Tecnología y Telecomunicaiones.</p>
<p>EL PNDT definirá la politica pública en los proximos 6 años en material de telecomunicaciones. Esta primera sesión participaron más de 150 personas del gobierno, las universidades, organizaciones no gubernamentales, así como de las cámaras y empresas del sector de telecomunicaciones.</p>',1),
( N'La UCR desarrolla un prototipo de aplicación móvil para el equipo PRIME del CEACO',NULL, N'2021-05-02 00:00:00',N'2021-05-02 00:00:00',N'2021-05-02 00:00:00',N'<p>Cada vez que la Caja Costarricense de Seguro Social (CCSS) identifica a una persona que está en condición muy delicada debido al coronavirus, en cualquier parte del país, activa el protocolo de atención que incluye el traslado hacia el Centro Especializado para la Atención de Pacientes con COVID-19 (CEACO). Esta acción recae sobre el equipo PRIME (Primera Intervención Médica Especializada), un grupo de especialistas compuesto por tres emergenciólogos, siete enfermeros, siete terapeutas respiratorios, cinco asistentes de pacientes y cinco conductores, y que tiene su base de operaciones en el CEACO.<br>&nbsp;<br>Como parte de las labores de cada misión, los profesionales del PRIME tienen que completar una serie de chequeos rigurosos y cumplir con los diferentes protocolos sanitarios, que están establecidos dentro de las medidas de seguridad implementadas por las autoridades de salud para que no se extienda el contagio. El equipo PRIME recurría a aplicaciones de mensajería de uso común para coordinar los traslados y registrar la información relacionada con los diferentes protocolos sanitarios. Al ser un procedimiento importante y necesario, pero tedioso y complejo de realizar utilizando estas aplicaciones, los responsables del equipo PRIME se pusieron en contacto con la Escuela de Ciencias de la Computación e Informática (ECCI) y el Centro de Investigaciones en Tecnologías de la Información y Comunicación (CITIC) de la Universidad de Costa Rica (UCR), para analizar la posibilidad de crear un prototipo de aplicación móvil multiplataforma que automatizara estos procesos.</p><p><br></p>
<p>Para dar respuesta a este reto, se conformó un equipo de trabajo compuesto por ocho estudiantes avanzados de la ECCI: Jose Mejías, Erik Kühlmann, Francisco Durán, Josué Amador, Ignacio Arroyo, Roy Padilla, Steven Fernández y Elián Ortega&nbsp;apoyados por cuatro investigadores y docentes del CITIC y de la Escuela de Ciencias de la Computación e Informática (ECCI): Dr. Marcelo Jenkins Coronas, Dr. Christian Quesada-López, Lic. Leonardo Villalobos Arias y Dr. Adrián Lara Petitdemange.</p><p><br></p>
<p>El Dr. Marcelo Jenkins, director del CITIC, explicó que lograron desarrollar un prototipo de aplicación para celulares y tabletas, que incluso ya fue entregado al equipo PRIME para que lo utilicen y obtener una retroalimentación valiosa sobre la percepción y capacidad de trabajo de esta herramienta, con el objetivo de perfeccionarla.</p><p><br></p>
<p>La primera versión se elaboró durante agosto y setiembre del 2020, y luego se entregó al equipo PRIME para una prueba de campo. La segunda versión se terminó de confeccionar en diciembre del 2020, para continuar con el estudio de su desempeño hasta la fecha.</p>
<p>“Este prototipo de aplicación corre tanto en Android como en iOS y se mantiene en estos momentos en fase de prueba. Se hizo con una arquitectura abierta para que a futuro se pueda extender con mayor funcionalidad y que tenga la posibilidad de integrarse con otras aplicaciones, como por ejemplo el EDUS (Expediente Digital Único en Salud) de la CCSS. Así todo estaría encadenado de manera virtual y la información se registraría directamente en el expediente digital de la Caja”, indicó Jenkins.</p><p><br></p>
<p>El Director del CITIC resaltó que se trata de otro proyecto más de contribución que realiza la UCR al país en la lucha contra la pandemia, pues no se cobró por brindar este servicio, los investigadores donaron su tiempo y conocimientos, y los estudiantes trabajaron mediante nombramientos de horas asistentes pagadas por el CITIC, “todo el costo fue asumido por el CITIC y la aplicación fue donada a la CCSS”, afirmó Jenkins.</p>
<p>Esta iniciativa se incluyó dentro del proyecto CASTIC ED-3000 que está inscrito en la Vicerrectoría de Acción Social (VAS) de la UCR.</p><p><br></p>
<h3><span style="font-weight: bold;">Características y validación de la herramienta</span></h3><div><br></div>
<p>El Dr. Christian Quesada-López, mencionó que este prototipo funciona como una herramienta informática de apoyo logístico, dirigida a almacenar de forma eficiente todo lo que sucede durante el traslado de un paciente con COVID-19 hacia el CEACO.</p><p><br></p>
<p>“Esta última versión que desarrollamos tuvo algunas consideraciones especiales en cuanto al uso de la información. Por ejemplo, sólo pueden tener acceso los usuarios registrados del equipo PRIME, además sólo se manejan los datos sobre los traslados y las listas de chequeos de los protocolos sanitarios. Es importante aclarar que no se incluyen los datos sensibles de los pacientes”, subrayó Quesada-López.</p><p><br></p>
<p>En resumen, este proyecto ideado por el CITIC y la ECCI incluye el prototipo de aplicación móvil con la que pueden dar seguimiento a cada misión en tiempo real y apoyar en la toma de decisiones; y una base de datos centralizada para agilizar la gestión de la información de los traslados, constatar la aplicación de los procedimientos médicos y sanitarios pertinentes, y administrar el contenido multimedia.</p>
<p>El Lic. Pablo Alfaro Ulate, coordinador de terapia respiratoria del CEACO, comentó que este prototipo representa una herramienta de vital importancia, “pues facilita la forma en que se atienden los incidentes, la recopilación de la información y la capacidad resolutiva de este conjunto de profesionales en salud, quienes velan por la seguridad de los pacientes en estado delicado por el COVID-19”.</p><p><br></p>
<p>Finalmente, los investigadores y estudiantes de la UCR realizaron una evaluación presencial y anónima con 17 miembros del equipo PRIME en marzo del 2021, para poder medir la experiencia del usuario.</p><p><br></p>
<p>Un total de 16 especialistas completaron el instrumento que se utilizó, mientras que sólo uno lo hizo parcialmente. En general, 16 de los 17 usuarios (uno no respondió) consideran que el prototipo sí cumple con los objetivos y funcionalidades requeridas, además contestaron que se sentirían cómodos utilizándola y sí recomendarían su uso. Por último, 15 respondieron que la aplicación es fácil de utilizar.</p><p><br></p>
<p>El Dr. Max Morales Mora, director del equipo PRIME, expresó que esta aplicación ha cumplido con todas las expectativas y abre una puerta para que se puedan gestionar futuras colaboraciones entre la UCR y la CCSS. “Agradecemos la enorme cooperación realizada por el equipo de trabajo del CITIC y de la ECCI, pues ha sido el pilar del éxito de esta gestión entre dos instituciones hermanas”, concluyó.</p><p><br></p>
<h4><span style="font-weight: bold;">Estudiantes que desarrollaron el prototipo</span></h4>
<ul><li>Jose Mejías</li><li>Erik Kühlmann</li><li>Francisco Durán</li><li>Josué Amador</li><li>Ignacio Arroyo</li><li>Roy Padilla</li><li>Steven Fernández</li><li>Elián Ortega</li>
<li>






<br></li></ul><h4><span style="font-weight: bold;">Docentes que asesoraron a los estudiantes</span></h4>
<ul><li>Marcelo Jenkins</li><li>Christian Quesada-López</li><li>Leonardo Villalobos&nbsp;</li><li>Adrián Lara</li>
<li>


<br></li></ul><h4><span style="font-weight: bold;">Funcionarios de CEACO que asesoraron sobre los requerimientos del prototipo</span></h4>
<ul><li>Pablo Alfaro</li>
<li>Max Morales</li>
</ul><p>&nbsp;</p>
<p><b>Por Otto Salas, Oficina de Divulgacióne&nbsp;Informacion, UCR</b></p>',1),
( N'Dra. Gabriela Marín impartió charla sobre "Cómo investigar usando ciencias del diseño"',NULL, N'2021-03-12 00:00:00',N'2021-03-12 00:00:00',N'2021-03-12 00:00:00',N'El pasado Martes 02 de Marzo del 2021, la investigadora Dra. Gabriela Marín impartió la charla "Cómo investigar usando ciencias del diseño" por medio de la plataforma Zoom. La actividad fue organizada por Universidad Técnica Nacional.',1),
( N'Dra. Gabriela Marín participó VI Congreso Internacional en Tecnologías de la Información y Comunicación',NULL, N'2021-02-26 00:00:00',N'2021-02-26 00:00:00',N'2021-02-26 00:00:00',N'La Dra. Gabriela Marín participó en el VI Congreso Internacional en Tecnologías de la Información y Comunicación, transmitido del 25-26 de Febrero por medio de la plataforma Zoom. Cabe destacar que la Dra. Marín se desempeño como chair durante la presentación de la línea temática "Technology computation". Adicionalmente, es importante mencionar que otros investigadores del CITIC como el Mag. Jose Antonio Brenes estuvieron presentes como parte del público.',1),
( N'Investigadora del CITIC participó en el "VIII Simposio Informática Empresarial y Desarrollo Nacional"',NULL, N'2020-11-23 00:00:00',N'2020-11-23 00:00:00',N'2020-11-23 00:00:00',N'El pasado Jueves 19 de noviembre del 2020, la investigadora Dr. Kryscia Ramírez Benavides participo en el "VIII Simposio Informática Empresarial y Desarrollo Nacional" por medio de la plataforma Zoom. La actividad fue organizada por la Sede Guanacaste de la Universidad de Costa Rica y la Dr. Ramirez impartió la charla inagural "Interacción Humano-Robot".',1),
( N'Investigadores de CITIC participaron en la XXIII Conferencia Iberoamericana de Ingeniería de Software (CIbSE 2020)',NULL, N'2020-11-11 00:00:00',N'2020-11-11 00:00:00',N'2020-11-11 00:00:00',N'<p>Investigadores del CITIC participaron en la&nbsp;<strong>XXIII Conferencia Iberoamericana en Ingeniería de Software (CIbSE 2020)</strong>, la cual se llevó&nbsp;a cabo virtualmente del 9&nbsp;al 13&nbsp;de noviembre, por medio de la plataforma Youtube y Zoom.&nbsp;CIbSE es el foro de investigación de Ingeniería de Software (Software Engineering) líder en Iberoamérica. El principal objetivo de esta conferencia es promover la investigación científica de alta calidad en países de Iberoamérica, y así,&nbsp; dar soporte a los investigadores en esta comunidad en la publicación y discusión de su trabajo. Además, la conferencia fomenta la colaboración y la producción científica entre los académicos, estudiantes y la industria del software.</p>
<p>Los trabajos presentados&nbsp;por los investigadores son los siguientes:</p>
<p><b>1. Un estudio comparativo sobre la medición del tamaño funcional del software para respaldar la estimación del esfuerzo de forma ágil.</b></p>
<ul><li>Fabián Ugalde</li>
<li>Christian Quesada-López</li>
<li>Alexandra Martínez&nbsp;</li>
<li>Marcelo Jenkins</li>
</ul><p><b>2. Herramientas para problemas de seguridad automatizados en aplicaciones web: un mapa sistemático de la literatura</b></p>
<ul><li>Elizabeth Gamboa</li>
<li>Christian Quesada-López</li>
<li>Alexandra Martínez</li>
<li>Marcelo Jenkins&nbsp;</li>
</ul><p><strong>3.&nbsp;Técnicas de aprendizaje automático para mejorar el rendimiento de las aplicaciones web: un mapa sistemático de la literatura</strong></p>
<ul><li>Jean Carlo Zúñiga-Madrigal</li>
<li>Christian Quesada-López</li>
<li>Marcelo Jenkins</li>
<li>Alexandra Martínez</li>
</ul><p>&nbsp;</p>',1)
--( N'','img/news/1.png', N'2021-10-21 00:00:00',N'2021-10-21 00:00:00',N'2021-10-21 00:00:00',N'',1),
)
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
    ('img/news/9.png',9),
    ('img/news/10.png',10)
)
AS SOURCE ([Path],[NewsID]) ON TARGET.[Path] = SOURCE.[Path]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Path],[NewsID]) VALUES ([Path],[NewsID]);

EXEC SP_AddMainImageToNews @image = 1, @news = 1;
EXEC SP_AddMainImageToNews @image = 2, @news = 2;
EXEC SP_AddMainImageToNews @image = 3, @news = 3;
EXEC SP_AddMainImageToNews @image = 4, @news = 4;
EXEC SP_AddMainImageToNews @image = 5, @news = 5;
EXEC SP_AddMainImageToNews @image = 6, @news = 6;
EXEC SP_AddMainImageToNews @image = 7, @news = 7;
EXEC SP_AddMainImageToNews @image = 8, @news = 8;
EXEC SP_AddMainImageToNews @image = 9, @news = 9;
EXEC SP_AddMainImageToNews @image = 10, @news = 10;

MERGE INTO Person AS Target 
USING (VALUES 
    --('correo', 'nombre','primerApellido', 'segundoApellido'), 
    ('arturo.camacho@ecci.ucr.ac.cr', 'Arturo', 'Camacho', 'Lozano'),
    ('davidviquez21@gmail.com', 'David', 'Víquez', 'León'),
    ('adrvegve@gmail.com', 'Adrián', 'Vega', 'Vega'),
    ('andrea.chaconpaez@ucr.ac.cr', 'Andrea', 'Chacón', 'Páez'),
    ('braulio.solano@ecci.ucr.ac.cr','Braulio', 'Solano', 'Rojas'),
    ('cristian.martinezhernandez@ucr.ac.cr', 'Cristian', 'Martínez', 'Hernández'),
    ('donny.nunez@ecci.ucr.ac.cr', 'Donny', 'Nuñes', 'del Rosario'),
    ('andrea.alvaradoacon@ucr.ac.cr','Andrea', 'Alvarado', 'Acon'),
    ('GREIVIN.SANCHEZGARITA@ucr.ac.cr', 'Greivin', 'Sanchez', 'Garita'),
    ('SEBASTIAN.MONTEROCASTRO@ucr.ac.cr','Sebastian', 'Montero', 'Castro'),
    ('DYLAN.ARIAS@ucr.ac.cr','Dylan','Arias','Rivera'),
    ('CARLOS.MORAMEMBRENO@ucr.ac.cr', 'Carlos', 'Mora', 'Membreño'),
    ('edgar.casasola@ucr.ac.cr','Edgar','Casasola','Murillo'),
    ('gabriela.barrantes@ucr.ac.cr','Gabriela','Barrantes','Sliesarieva'),
    ('gabriela.marin@ucr.ac.cr','Gabriela','Marín','Raventós'),
    ('jeisson.hidalgo@ucr.ac.cr','Jeisson','Hidalgo','Cespedes'),
    ('joseantonio.brenes@ucr.ac.cr','Jose Antonio','Brenes','Carranza'),
    ('kryscia.ramirez@ucr.ac.cr','Kryscia','Ramírez','Benavides'),
    ('luis.esquivel@ucr.ac.cr','Luis Gustavo','Esquivel','Quirós'),
    ('ricardo.villalon@ucr.ac.cr','Ricardo','Villalón','Fonseca'),
    ('vladimir.lara@ucr.ac.cr','Vladimir','Lara','Villagrán'),
    --
    ('marcelo.jenkings@ucr.ac.cr','Marcelo','Jenkins','Coronas'), --ESTE
    ('alexandra.martinez@ecci.ucr.ac.cr', 'Alexandra', 'Martínez', 'Porras'),--ESTE
    ('LEONARDO.VILLALOBOSARIAS@ucr.ac.cr','Leonardo','Villalobos','Arias'),
    ('sivana.hamercampos@ucr.ac.cr','Sivana','Hamer','Campos'),
    ('cristian.quesadalopez@ucr.ac.cr', 'Cristian', 'Quesada', 'López'), --ESTE
    ('ADRIAN.LARA@ucr.ac.cr', 'Adrián', 'Lara', 'Petitdemange'), --ESTE
    ('jose.guevaracoto@ucr.ac.cr','Jose','Guevara','Coto'),
    ('abel.mendezporras@ucr.ac.cr','Abel','Méndez','Porras'),
    ('juan.murillomorera@ucr.ac.cr','Juan','Murillo','Morera'),
    ('lauriewilliams@gmail.com','Laurie','Williams',' '),
    ('oscarpastorlopez@gmail.com','Oscar','Pastor','López'),
    ('guilhermehortatracassos@gmail.com','Guilherme','Horta','Travassos'),
    ('melissajensen@gmail.com','Melissa','Jenses','Madrigal'),
    ('erikahernandez@ucr.ac.cr','Erika','Hernández','Agüero'),
    ('pablo.ramirezmendez@ucr.ac.cr','Pablo','Ramírez','Méndez'),
    ('jose.mejiasrojas@ucr.ac.cr','Jose','Mejías','Rojas'),
    ('denisse.madrigalsanchez@ucr.ac.cr','Denisse','Madrigal','Sanchez'),
    ('luis.salasvillalobos@ucr.ac.cr','Luis','Salas','Villalobos'),
    ('andres.martinezmesen@ucr.ac.cr','Andrés','Martínez','Mesén'),
    ('rebeca.obandovazquez@ucr.ac.cr','Rebeca','Obando','Vázquez'),
    ('erik.kuhkmann@ucr.ac.cr','Erik','Kuhkmann',' '),
    ('juan.valverde@ucr.ac.cr','Juan','Valverde',' '),
    ('antonio.badilla@ucr.ac.cr','Antonio','Badilla',' '),
    ('danel.salazar@ucr.ac.cr','Daniel','Salazar',' '),
    ('ricardo.franco@ucr.ac.cr','Ricardo','Franco',' '),
    ('stevenfernandez@ucr.ac.cr','Steven','Fernández',' '),
    ('gustavoesquivel@ucr.ac.cr','Gustavo','Esquivel', ' '),
    ('marta.calderon@ecci.ucr.ac.cr','Marta','Calderon',' '),
	('lyon.villalobos@gmail.com','Leonardo','Villalobos',' '),
	('denisse.gmadrigal@gmail.com','Denisse','Madrigal',' '),
	('brenda.aymerich@ucr.ac.cr','Brenda','Aymerich',' '),
	('IGNACIO.DIAZOREIRO@ucr.ac.cr','Ignacio','Díaz',' '),
	('julio.guzman@ucr.ac.cr','Julio','Guzmán',' '),
	('gustavo.lopez_h@ucr.ac.cr','Gustavo','López',' '),
	('diana.garbanzo@ucr.ac.cr','Diana','Garbanzo',' '),
	('carlos.castro@ecci.ucr.ac.cr','Carlos','Castro',' '),
	('ruizble@yahoo.com','Sebastián','Ruíz',' '),
	('juan.fonsecasolis@ucr.ac.cr','Juan Manuel','Fonseca',' '),
	('luis.guerrero@ecci.ucr.ac.cr','Luis Alberto','Guerrero',' '),
	('luis.quesada@ecci.ucr.ac.cr','Luis','Quesada',' ')


    ) 
AS Source ([Email], [FirstName], [FirstLastName],[SecondLastName]) ON Target.[Email] = Source.[Email] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Email], [FirstName], [FirstLastName],[SecondLastName]) VALUES ([Email], [FirstName], [FirstLastName],[SecondLastName]);

MERGE INTO Collaborator AS Target 
USING (VALUES 
    ('gustavoesquivel@ucr.ac.cr','InvestigadorPrincipal'),
    ('arturo.camacho@ecci.ucr.ac.cr','Investigador Principal'),
    ('davidviquez21@gmail.com','Líder de Proyectos'),
    ('adrvegve@gmail.com','Líder de Proyectos'),
    ('andrea.chaconpaez@ucr.ac.cr','Asistente de Proyectos'),
    ('braulio.solano@ecci.ucr.ac.cr','Desarrollador de Software'),
    ('cristian.martinezhernandez@ucr.ac.cr','Desarrollador de Software'),
    ('donny.nunez@ecci.ucr.ac.cr','Investigador Secundario'),
    ('andrea.alvaradoacon@ucr.ac.cr','Estudiante'),
    ('GREIVIN.SANCHEZGARITA@ucr.ac.cr','Estudiante'),
    ('SEBASTIAN.MONTEROCASTRO@ucr.ac.cr','Estudiante'),
    ('DYLAN.ARIAS@ucr.ac.cr','Estudiante'),
    ('CARLOS.MORAMEMBRENO@ucr.ac.cr','Estudiante'),
    ('edgar.casasola@ucr.ac.cr','Docente Investigador'),
    ('gabriela.barrantes@ucr.ac.cr','Docente Investigadora'),
    ('gabriela.marin@ucr.ac.cr','Desarrolladora de Software'),
    ('jeisson.hidalgo@ucr.ac.cr','Investigador Principal'),
    ('joseantonio.brenes@ucr.ac.cr','Experto en Redes'),
    ('kryscia.ramirez@ucr.ac.cr','Subdirectora del CITIC'),
    ('luis.esquivel@ucr.ac.cr','Investigador Secundario'),
    ('ricardo.villalon@ucr.ac.cr','Docente Investigador'),
    ('vladimir.lara@ucr.ac.cr','Desarrollador Principal'),
    --
    ('marcelo.jenkings@ucr.ac.cr','Investigador Principal'), 
    ('alexandra.martinez@ecci.ucr.ac.cr','Investigadora Principal'),
    ('LEONARDO.VILLALOBOSARIAS@ucr.ac.cr','Investigador Principal'),
    ('sivana.hamercampos@ucr.ac.cr','Investigadora Principal'),
    ('cristian.quesadalopez@ucr.ac.cr', 'Investigador Principal'), 
    ('ADRIAN.LARA@ucr.ac.cr', 'Colaborador'), 
    ('jose.guevaracoto@ucr.ac.cr','Colaborador'),
    ('abel.mendezporras@ucr.ac.cr','Colaborador'),
    ('juan.murillomorera@ucr.ac.cr','Colaborador'),
    ('lauriewilliams@gmail.com','Colaboradora'),
    ('oscarpastorlopez@gmail.com','Colaborador'),
    ('guilhermehortatracassos@gmail.com','Colaborador'),
    ('melissajensen@gmail.com','Colaboradora'),
    ('erikahernandez@ucr.ac.cr','Estudiante Doctorado'),
    ('pablo.ramirezmendez@ucr.ac.cr','Estudiante Maestría Académica'),
    ('jose.mejiasrojas@ucr.ac.cr','Estudiante Maestría Académica'),
    ('denisse.madrigalsanchez@ucr.ac.cr','Estudiante Maestría Profesional'),
    ('luis.salasvillalobos@ucr.ac.cr','Estudiante Maestría Profesional'),
    ('andres.martinezmesen@ucr.ac.cr','Estudiantes Maestría Profesional'),
    ('rebeca.obandovazquez@ucr.ac.cr','Estudiante Maestría Profesional'),
    ('erik.kuhkmann@ucr.ac.cr','Estudiante Grado'),
    ('juan.valverde@ucr.ac.cr','Estudiante Grado'),
    ('antonio.badilla@ucr.ac.cr','Estudiante Grado'),
    ('danel.salazar@ucr.ac.cr','Estudiante Grado'),
    ('ricardo.franco@ucr.ac.cr','Estudiante Grado'),
    ('stevenfernandez@ucr.ac.cr','Estudiante Grado'),

    ('marta.calderon@ecci.ucr.ac.cr','Colaborador'),
	('lyon.villalobos@gmail.com','Colaborador'),
	('denisse.gmadrigal@gmail.com','Colaborador'),
	('brenda.aymerich@ucr.ac.cr','Colaborador'),
	('IGNACIO.DIAZOREIRO@ucr.ac.cr','Colaborador'),
	('julio.guzman@ucr.ac.cr','Colaborador'),
	('gustavo.lopez_h@ucr.ac.cr','Colaborador'),
	('diana.garbanzo@ucr.ac.cr','Colaborador'),
	('carlos.castro@ecci.ucr.ac.cr','Colaborador'),
	('ruizble@yahoo.com','Colaborador'),
	('juan.fonsecasolis@ucr.ac.cr','Colaborador'),
	('luis.guerrero@ecci.ucr.ac.cr','Colaborador'),
	('luis.quesada@ecci.ucr.ac.cr','Colaborador')
    ) 
AS Source ([Email],[Role]) ON Target.[Email] = Source.[Email] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Email],[Role]) VALUES ([Email],[Role]);



MERGE INTO AcademicProfile AS Target 
USING (VALUES 

    ('gustavoesquivel@ucr.ac.cr', 'Biography', './img/ProfilePictures/default.png','Dr.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('arturo.camacho@ecci.ucr.ac.cr', 'Biography', './img/ProfilePictures/arturocamacho.png','Dr.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('davidviquez21@gmail.com', 'Biography', './img/ProfilePictures/default.png','Mag.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('adrvegve@gmail.com', 'Biography', './img/ProfilePictures/default.png',null,'https://www.facebook.com/', null, 'https://www.linkedin.com/', 'N/A'),
    ('andrea.chaconpaez@ucr.ac.cr', 'Biography', './img/ProfilePictures/default.png','Mag.','https://www.facebook.com/', 'https://github.com/', null, 'N/A'),
    ('braulio.solano@ecci.ucr.ac.cr','Biography','./img/ProfilePictures/brauliosolano.png','MSc.', null , 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('cristian.martinezhernandez@ucr.ac.cr', 'Biography','./img/ProfilePictures/default.png',null,'https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('donny.nunez@ecci.ucr.ac.cr', 'Biography','./img/ProfilePictures/default.png','Dr.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('andrea.alvaradoacon@ucr.ac.cr', 'Biography', './img/ProfilePictures/andreaalvarado.png',null,'https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('GREIVIN.SANCHEZGARITA@ucr.ac.cr', 'Biography', './img/ProfilePictures/greivinsanchez.png',null,null, 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('SEBASTIAN.MONTEROCASTRO@ucr.ac.cr', 'Biography', './img/ProfilePictures/sebastianmontero.png',null,'https://www.facebook.com/', null, 'https://www.linkedin.com/', 'N/A'),
    ('DYLAN.ARIAS@ucr.ac.cr', 'Biography', './img/ProfilePictures/default.png',null,'https://www.facebook.com/', 'https://github.com/', null, 'N/A'),
    ('CARLOS.MORAMEMBRENO@ucr.ac.cr', 'Biography', './img/ProfilePictures/carlosmora.png',null,null, null, 'https://www.linkedin.com/', 'N/A'),
    ('edgar.casasola@ucr.ac.cr','Biography','./img/ProfilePictures/edgarcasasola.png','Dr.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('gabriela.barrantes@ucr.ac.cr','Biography','./img/ProfilePictures/gabrielabarrantes.png',null,'https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('gabriela.marin@ucr.ac.cr','Biography','./img/ProfilePictures/gabrielamarinraventos.png',null,'https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('jeisson.hidalgo@ucr.ac.cr','Biography','./img/ProfilePictures/jeissonhidalgo.png','Dr.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('joseantonio.brenes@ucr.ac.cr','Biography','./img/ProfilePictures/joseantoniobrenes.png','MSc.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('kryscia.ramirez@ucr.ac.cr','Biography','./img/ProfilePictures/krysciaramirez.png','Dra.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('luis.esquivel@ucr.ac.cr','Biography','./img/ProfilePictures/luisgustavoesquivelquiros.png','Mag.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('ricardo.villalon@ucr.ac.cr','Biography','./img/ProfilePictures/ricardovillalonfonseca.png','MSc.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('vladimir.lara@ucr.ac.cr','Biography','./img/ProfilePictures/vladimirlara.png','Dr.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),

    --Academic Profile
    ('marcelo.jenkings@ucr.ac.cr','Biography','./img/ProfilePictures/marcelojenkins.png','Dr.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'Universidad de Delaware, U.S.A., 1992'), --ESTE
    ('alexandra.martinez@ecci.ucr.ac.cr', 'Biography','./img/ProfilePictures/alexandramartinez.png','Dra.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),--ESTE
    ('LEONARDO.VILLALOBOSARIAS@ucr.ac.cr','Biography','./img/ProfilePictures/default.png','Msc.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('sivana.hamercampos@ucr.ac.cr','Biography','./img/ProfilePictures/default.png','Bach.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('cristian.quesadalopez@ucr.ac.cr', 'Biography','./img/ProfilePictures/christianquesadalopez.png','Dr.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'), --ESTE
    ('ADRIAN.LARA@ucr.ac.cr', 'Biography','./img/ProfilePictures/adrianlara.png','Dr.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'), --ESTE
    ('jose.guevaracoto@ucr.ac.cr','Biography','./img/ProfilePictures/default.png','Dr.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('abel.mendezporras@ucr.ac.cr','Biography','./img/ProfilePictures/default.png','Dr.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('juan.murillomorera@ucr.ac.cr','Biography','./img/ProfilePictures/default.png','Dr.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('lauriewilliams@gmail.com','Biography','./img/ProfilePictures/default.png','Dra.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('oscarpastorlopez@gmail.com','Biography','./img/ProfilePictures/default.png','Dr.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('guilhermehortatracassos@gmail.com','Biography','./img/ProfilePictures/default.png','Dr.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('melissajensen@gmail.com','Biography','./img/ProfilePictures/default.png','Dra.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('erikahernandez@ucr.ac.cr','Biography','./img/ProfilePictures/default.png','Mag.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('pablo.ramirezmendez@ucr.ac.cr','Biography','./img/ProfilePictures/default.png','Bach.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('jose.mejiasrojas@ucr.ac.cr','Biography','./img/ProfilePictures/default.png','Bach.',null, 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('denisse.madrigalsanchez@ucr.ac.cr','Biography','./img/ProfilePictures/default.png','Mag.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('luis.salasvillalobos@ucr.ac.cr','Biography','./img/ProfilePictures/default.png','Mag.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('andres.martinezmesen@ucr.ac.cr','Biography','./img/ProfilePictures/default.png','Mag.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('rebeca.obandovazquez@ucr.ac.cr','Biography','./img/ProfilePictures/default.png','Mag.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('erik.kuhkmann@ucr.ac.cr','Biography','./img/ProfilePictures/default.png',null,'https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('juan.valverde@ucr.ac.cr','Biography','./img/ProfilePictures/default.png',null,'https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('antonio.badilla@ucr.ac.cr','Biography','./img/ProfilePictures/default.png',null,'https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('danel.salazar@ucr.ac.cr','Biography','./img/ProfilePictures/default.png',null,'https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('ricardo.franco@ucr.ac.cr','Biography','./img/ProfilePictures/default.png',null,'https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/', 'N/A'),
    ('stevenfernandez@ucr.ac.cr','Biography','./img/ProfilePictures/default.png',null,'https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/,', 'N/A'),
    ('marta.calderon@ecci.ucr.ac.cr','Biography','./img/ProfilePictures/default.png','M.Sc.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/,', 'N/A'),
	('lyon.villalobos@gmail.com','Biography','./img/ProfilePictures/default.png','Lic','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/,', 'N/A'),
	('denisse.gmadrigal@gmail.com','Biography','./img/ProfilePictures/default.png',null,'https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/,', 'N/A'),
	('brenda.aymerich@ucr.ac.cr','Biography','./img/ProfilePictures/default.png',null,'https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/,', 'N/A'),
	('IGNACIO.DIAZOREIRO@ucr.ac.cr','Biography','./img/ProfilePictures/default.png','M.Sc.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/,', 'N/A'),
	('julio.guzman@ucr.ac.cr','Biography','./img/ProfilePictures/default.png','M.Sc.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/,', 'N/A'),
	('gustavo.lopez_h@ucr.ac.cr','Biography','./img/ProfilePictures/default.png','Dr.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/,', 'N/A'),
	('diana.garbanzo@ucr.ac.cr','Biography','./img/ProfilePictures/default.png',null,'https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/,', 'N/A'),
	('carlos.castro@ecci.ucr.ac.cr','Biography','./img/ProfilePictures/default.png','Dr.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/,', 'N/A'),
	('ruizble@yahoo.com','Biography','./img/ProfilePictures/default.png',null,'https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/,', 'N/A'),
	('juan.fonsecasolis@ucr.ac.cr','Biography','./img/ProfilePictures/default.png','M.Sc.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/,', 'N/A'),
	('luis.guerrero@ecci.ucr.ac.cr','Biography','./img/ProfilePictures/default.png','Dr.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/,', 'N/A'),
	('luis.quesada@ecci.ucr.ac.cr','Biography','./img/ProfilePictures/default.png','Dr.','https://www.facebook.com/', 'https://github.com/', 'https://www.linkedin.com/,', 'N/A')
    ) 
AS Source ([Email], [Biography], [ProfilePic],[Degree], [FacebookLink], [GitHubLink], [LinkedInLink], [Title]) ON Target.[Email] = Source.[Email] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Email], [Biography], [ProfilePic],[Degree], [FacebookLink], [GitHubLink], [LinkedInLink], [Title]) VALUES ([Email], [Biography], [ProfilePic],[Degree], [FacebookLink], [GitHubLink], [LinkedInLink], [Title]);


MERGE INTO AcademicUnit AS Target 
USING (VALUES 
('Centro de Investigaciones en Tecnologías de la Información y Comunicación')
)
AS Source ([Name]) ON Target.[Name] = Source.[Name] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Name]) VALUES ([Name]);


MERGE INTO PersonWorksForUnit AS Target 
USING (VALUES 
    ('gustavoesquivel@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('arturo.camacho@ecci.ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('davidviquez21@gmail.com','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('adrvegve@gmail.com','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('andrea.chaconpaez@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('braulio.solano@ecci.ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('cristian.martinezhernandez@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('donny.nunez@ecci.ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('andrea.alvaradoacon@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('GREIVIN.SANCHEZGARITA@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('SEBASTIAN.MONTEROCASTRO@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('DYLAN.ARIAS@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('CARLOS.MORAMEMBRENO@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('edgar.casasola@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('gabriela.barrantes@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('gabriela.marin@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('jeisson.hidalgo@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('joseantonio.brenes@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('kryscia.ramirez@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('luis.esquivel@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('ricardo.villalon@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('vladimir.lara@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('marcelo.jenkings@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'), 
    ('alexandra.martinez@ecci.ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('LEONARDO.VILLALOBOSARIAS@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('sivana.hamercampos@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('cristian.quesadalopez@ucr.ac.cr', 'Centro de Investigaciones en Tecnologías de la Información y Comunicación'), 
    ('ADRIAN.LARA@ucr.ac.cr', 'Centro de Investigaciones en Tecnologías de la Información y Comunicación'), 
    ('jose.guevaracoto@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('abel.mendezporras@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('juan.murillomorera@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('lauriewilliams@gmail.com','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('oscarpastorlopez@gmail.com','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('guilhermehortatracassos@gmail.com','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('melissajensen@gmail.com','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('erikahernandez@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('pablo.ramirezmendez@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('jose.mejiasrojas@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('denisse.madrigalsanchez@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('luis.salasvillalobos@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('andres.martinezmesen@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('rebeca.obandovazquez@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('erik.kuhkmann@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('juan.valverde@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('antonio.badilla@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('danel.salazar@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('ricardo.franco@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('stevenfernandez@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
    ('marta.calderon@ecci.ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
	('lyon.villalobos@gmail.com','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
	('denisse.gmadrigal@gmail.com','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
	('brenda.aymerich@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
	('IGNACIO.DIAZOREIRO@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
	('julio.guzman@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
	('gustavo.lopez_h@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
	('diana.garbanzo@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
	('carlos.castro@ecci.ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
	('ruizble@yahoo.com','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
	('juan.fonsecasolis@ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
	('luis.guerrero@ecci.ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación'),
	('luis.quesada@ecci.ucr.ac.cr','Centro de Investigaciones en Tecnologías de la Información y Comunicación')
)
AS Source ([Email],[UnitName]) ON Target.[Email] = Source.[Email] and Target.[UnitName] = Source.[UnitName]
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Email],[UnitName]) VALUES ([Email],[UnitName]);


MERGE INTO Investigator AS Target 
USING (VALUES 
    ('gustavoesquivel@ucr.ac.cr'),
    ('ADRIAN.LARA@ucr.ac.cr'),
    ('arturo.camacho@ecci.ucr.ac.cr'),
    ('cristian.quesadalopez@ucr.ac.cr'),
    ('davidviquez21@gmail.com'),
    ('adrvegve@gmail.com'),
    ('andrea.chaconpaez@ucr.ac.cr'),
    ('braulio.solano@ecci.ucr.ac.cr'),
    ('cristian.martinezhernandez@ucr.ac.cr'),
    ('donny.nunez@ecci.ucr.ac.cr'),
    ('edgar.casasola@ucr.ac.cr'),
    ('gabriela.barrantes@ucr.ac.cr'),
    ('gabriela.marin@ucr.ac.cr'),
    ('jeisson.hidalgo@ucr.ac.cr'),
    ('joseantonio.brenes@ucr.ac.cr'),
    ('luis.esquivel@ucr.ac.cr'),
    ('ricardo.villalon@ucr.ac.cr'),
    ('vladimir.lara@ucr.ac.cr'),
    ('marcelo.jenkings@ucr.ac.cr'),
    ('alexandra.martinez@ecci.ucr.ac.cr'),
    ('leonardo.villalobosarias@ucr.ac.cr'),
    ('sivana.hamercampos@ucr.ac.cr'),
    ('jose.guevaracoto@ucr.ac.cr'),
    ('abel.mendezporras@ucr.ac.cr'),
    ('juan.murillomorera@ucr.ac.cr'),
    ('lauriewilliams@gmail.com'),
    ('oscarpastorlopez@gmail.com'),
    ('guilhermehortatracassos@gmail.com'),
    ('melissajensen@gmail.com'),
    ('luis.guerrero@ecci.ucr.ac.cr'),
    ('luis.quesada@ecci.ucr.ac.cr'),
    ('kryscia.ramirez@ucr.ac.cr'),
    ('gustavo.lopez_h@ucr.ac.cr')
    ) 
AS Source ([Email]) ON Target.[Email] = Source.[Email] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Email]) VALUES ([Email]);


MERGE INTO InvestigatorManagesGroup AS Target 
USING (VALUES 


    ('marcelo.jenkings@ucr.ac.cr',1, '2021-01-01', '2021-11-07'), 
    ('alexandra.martinez@ecci.ucr.ac.cr',1, '2021-01-01', '2021-11-07'),
    ('LEONARDO.VILLALOBOSARIAS@ucr.ac.cr',1, '2021-01-01', '2021-11-07'),
    ('sivana.hamercampos@ucr.ac.cr',1, '2021-01-01', '2021-11-07'),
    ('cristian.quesadalopez@ucr.ac.cr',1,'2021-01-01', '2020-04-07'),

    ('luis.guerrero@ecci.ucr.ac.cr',2, '2015-01-01', '2018-08-08'),
    ('luis.quesada@ecci.ucr.ac.cr',2, '2015-01-01', '2018-08-08'),
    ('kryscia.ramirez@ucr.ac.cr', 2, '2009-01-01', '2021-11-11'),
    ('gustavo.lopez_h@ucr.ac.cr',2, '2015-01-01', '2018-08-08'),

    
    ('gabriela.barrantes@ucr.ac.cr', 3, '2021-01-01', '2021-11-11'),
    ('ADRIAN.LARA@ucr.ac.cr', 3, '2021-01-01', '2021-11-07'),
    ('ricardo.villalon@ucr.ac.cr', 3, '2018-01-01', '2021-11-07'),
    ('gustavoesquivel@ucr.ac.cr',3, '2018-01-01', '2021-11-07')

) 
AS Source ([Email], [GroupId], [StartDate],[EndDate]) ON Target.[Email] = Source.[Email] and Target.[GroupId] = Source.[GroupId]
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Email], [GroupId], [StartDate],[EndDate]) VALUES ([Email], [GroupId], [StartDate],[EndDate]);

MERGE INTO Student AS Target 
USING (VALUES 
    ('andrea.alvaradoacon@ucr.ac.cr', 'B90272'),
    ('GREIVIN.SANCHEZGARITA@ucr.ac.cr', 'B97248'),
    ('SEBASTIAN.MONTEROCASTRO@ucr.ac.cr','B95016'),
    ('DYLAN.ARIAS@ucr.ac.cr','B90696'),
    ('CARLOS.MORAMEMBRENO@ucr.ac.cr','B54727'),

    ('erikahernandez@ucr.ac.cr','B11114'),
    ('pablo.ramirezmendez@ucr.ac.cr','B11115'),
    ('jose.mejiasrojas@ucr.ac.cr','B11116'),
    ('denisse.madrigalsanchez@ucr.ac.cr','B11117'),
    ('luis.salasvillalobos@ucr.ac.cr','B11118'),
    ('andres.martinezmesen@ucr.ac.cr','B11119'),
    ('rebeca.obandovazquez@ucr.ac.cr','B11121'),
    ('erik.kuhkmann@ucr.ac.cr','B11122'),
    ('juan.valverde@ucr.ac.cr','B11123'),
    ('antonio.badilla@ucr.ac.cr','B11124'),
    ('danel.salazar@ucr.ac.cr','B11125'),
    ('ricardo.franco@ucr.ac.cr','B11126'),
    ('stevenfernandez@ucr.ac.cr','B11127')
    ) 
AS Source ([Email],[StudentId]) ON Target.[Email] = Source.[Email] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Email],[StudentId]) VALUES ([Email],[StudentId]);


MERGE INTO CollaboratorPartOfProject AS Target 
USING (VALUES 
    --('correo', 'nombre','primerApellido', 'segundoApellido'), 
    ('jose.mejiasrojas@ucr.ac.cr', 1,'Investigador Principal'),
    ('cristian.quesadalopez@ucr.ac.cr', 1,'Colaborador Interno'),
    ('alexandra.martinez@ecci.ucr.ac.cr', 1,'Colaborador Interno'),

    ('LEONARDO.VILLALOBOSARIAS@ucr.ac.cr', 2,'Investigador Principal'),
    ('cristian.quesadalopez@ucr.ac.cr', 2,'Colaborador Interno'),
    ('alexandra.martinez@ecci.ucr.ac.cr', 2,'Colaborador Interno'),
    ('marcelo.jenkings@ucr.ac.cr', 2,'Colaborador Interno'),

    ('cristian.quesadalopez@ucr.ac.cr', 3,'Colaborador Interno'),
    ('denisse.madrigalsanchez@ucr.ac.cr', 3,'Investigador Principal'),
    ('marcelo.jenkings@ucr.ac.cr', 3,'Colaborador Interno'),

    ('cristian.quesadalopez@ucr.ac.cr', 4,'Colaborador Interno'),
    ('marcelo.jenkings@ucr.ac.cr', 4,'Investigador Principal'),

    ('sivana.hamercampos@ucr.ac.cr', 5,'Investigador Principal'),
    ('cristian.quesadalopez@ucr.ac.cr', 5,'Colaborador Interno'),
    ('marcelo.jenkings@ucr.ac.cr', 5,'Colaborador Interno')


    ) 
AS Source ([CollaboratorEmail], [InvestigationProjectId], [Role]) ON Target.[CollaboratorEmail] = Source.[CollaboratorEmail] and Target.[InvestigationProjectId] = Source.[InvestigationProjectId]
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([CollaboratorEmail], [InvestigationProjectId], [Role]) VALUES ([CollaboratorEmail], [InvestigationProjectId], [Role]);


MERGE INTO AuthorsPartOfThesis AS Target 
USING (VALUES 
    --('correo', 'nombre','primerApellido', 'segundoApellido'), 
    ('cristian.quesadalopez@ucr.ac.cr', 1,'Comité'),
    ('alexandra.martinez@ecci.ucr.ac.cr', 1,'Comité'),
    ('marcelo.jenkings@ucr.ac.cr', 1,'Director'),
    ('LEONARDO.VILLALOBOSARIAS@ucr.ac.cr', 1,'Autor'),

    ('cristian.quesadalopez@ucr.ac.cr', 2,'Comité'),
    ('alexandra.martinez@ecci.ucr.ac.cr', 2,'Comité'),
    ('marcelo.jenkings@ucr.ac.cr', 2,'Director'),
    ('LEONARDO.VILLALOBOSARIAS@ucr.ac.cr', 2,'Autor'),

    ('cristian.quesadalopez@ucr.ac.cr', 3,'Autor'),
     ('marcelo.jenkings@ucr.ac.cr', 3,'Director'),
     ('alexandra.martinez@ecci.ucr.ac.cr', 3,'Comité'),

    ('cristian.quesadalopez@ucr.ac.cr', 4,'Comité'),
    ('denisse.madrigalsanchez@ucr.ac.cr', 4,'Autor'),
    ('marcelo.jenkings@ucr.ac.cr', 4,'Director'),

    ('luis.salasvillalobos@ucr.ac.cr', 5,'Autor'),
    ('cristian.quesadalopez@ucr.ac.cr', 5,'Director'),
    ('marcelo.jenkings@ucr.ac.cr', 5,'Comité'),

    ('sivana.hamercampos@ucr.ac.cr', 6,'Autor'),
    ('cristian.quesadalopez@ucr.ac.cr', 6,'Director'),
    ('marcelo.jenkings@ucr.ac.cr', 6,'Comité'),

    ('andres.martinezmesen@ucr.ac.cr', 7,'Autor'),
    ('cristian.quesadalopez@ucr.ac.cr', 7,'Comité'),
    ('marcelo.jenkings@ucr.ac.cr', 7,'Director')
    ) 
AS Source ([CollaboratorEmail], [ThesisId], [Role]) ON Target.[CollaboratorEmail] = Source.[CollaboratorEmail] and Target.[ThesisId] = Source.[ThesisId]
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([CollaboratorEmail], [ThesisId], [Role]) VALUES ([CollaboratorEmail], [ThesisId], [Role]);

/*
MERGE INTO ThesisPartOfProject AS Target 
USING (VALUES 
    --('correo', 'nombre','primerApellido', 'segundoApellido'), 
    (2,1),
    (3,2),
    (4,3),
    (4,4),
    (4,5),
    (5,6),
    (5,7)
    )
AS Source ([InvestigationProjectId], [ThesisId]) ON Target.[InvestigationProjectId] = Source.[InvestigationProjectId] and Target.[ThesisId] = Source.[ThesisId]
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([InvestigationProjectId], [ThesisId]) VALUES ([InvestigationProjectId], [ThesisId]);
*/

MERGE INTO CollaboratorPartOfGroup AS Target 
USING (VALUES 
    --('correo', 'nombre','primerApellido', 'segundoApellido'), 
    
    ('marcelo.jenkings@ucr.ac.cr',1, '2021-01-01', '2021-11-07'), --ESTE
    ('alexandra.martinez@ecci.ucr.ac.cr',1, '2021-01-01', '2021-11-07'),--ESTE
    ('LEONARDO.VILLALOBOSARIAS@ucr.ac.cr',1, '2021-01-01', '2021-11-07'),
    ('sivana.hamercampos@ucr.ac.cr',1, '2021-01-01', '2021-11-07'),
    ('cristian.quesadalopez@ucr.ac.cr', 1, '2021-01-01', '2021-11-07'), --ESTE
    ('ADRIAN.LARA@ucr.ac.cr', 1, '2021-01-01', '2021-11-07'), --ESTE
    ('jose.guevaracoto@ucr.ac.cr',1, '2021-01-01', '2021-11-07'),
    ('abel.mendezporras@ucr.ac.cr',1, '2015-01-01', '2018-08-08'),
    ('juan.murillomorera@ucr.ac.cr',1, '2015-01-01', '2018-08-08'),
    ('lauriewilliams@gmail.com',1, '2015-01-01', '2018-08-08'),
    ('oscarpastorlopez@gmail.com',1, '2015-01-01', '2018-08-08'),
    ('guilhermehortatracassos@gmail.com',1, '2015-01-01', '2018-08-08'),
    ('melissajensen@gmail.com',1, '2015-01-01', '2018-08-08'),
    ('erikahernandez@ucr.ac.cr',1, '2015-01-01', '2018-08-08'),
    ('pablo.ramirezmendez@ucr.ac.cr',1, '2015-01-01', '2018-08-08'),
    ('jose.mejiasrojas@ucr.ac.cr',1, '2015-01-01', '2018-08-08'),
    ('denisse.madrigalsanchez@ucr.ac.cr',1, '2015-01-01', '2018-08-08'),
    ('luis.salasvillalobos@ucr.ac.cr',1, '2015-01-01', '2018-08-08'),
    ('andres.martinezmesen@ucr.ac.cr',1, '2015-01-01', '2018-08-08'),
    ('rebeca.obandovazquez@ucr.ac.cr',1, '2015-01-01', '2018-08-08'),
    ('erik.kuhkmann@ucr.ac.cr',1, '2015-01-01', '2018-08-08'),
    ('juan.valverde@ucr.ac.cr',1, '2015-01-01', '2018-08-08'),
    ('antonio.badilla@ucr.ac.cr',1, '2015-01-01', '2018-08-08'),
    ('danel.salazar@ucr.ac.cr',1, '2015-01-01', '2018-08-08'),
    ('ricardo.franco@ucr.ac.cr',1, '2015-01-01', '2018-08-08'),
    ('stevenfernandez@ucr.ac.cr',1, '2021-01-01', '2021-11-07'),
    
    ('luis.guerrero@ecci.ucr.ac.cr',2, '2015-01-01', '2018-08-08'),
    ('luis.quesada@ecci.ucr.ac.cr',2, '2015-01-01', '2018-08-08'),
    ('kryscia.ramirez@ucr.ac.cr', 2, '2009-01-01', '2021-11-11'),
    ('gustavo.lopez_h@ucr.ac.cr',2, '2015-01-01', '2018-08-08'),
    
    ('gabriela.barrantes@ucr.ac.cr', 3, '2021-01-01', '2021-11-11'),
    ('ADRIAN.LARA@ucr.ac.cr', 3, '2021-01-01', '2021-11-07'),
    ('ricardo.villalon@ucr.ac.cr', 3, '2018-01-01', '2021-11-07'),
    ('gustavoesquivel@ucr.ac.cr',3, '2018-01-01', '2021-11-07'),

    
    
    ('arturo.camacho@ecci.ucr.ac.cr', 2, '2021-01-01', '2021-11-08'),
    ('davidviquez21@gmail.com', 2, '2016-01-01', '2021-11-11'),
    ('adrvegve@gmail.com',3, '2013-01-01', '2021-11-03'),
    ('andrea.chaconpaez@ucr.ac.cr', 2, '2021-01-01', '2021-09-11'),
    ('braulio.solano@ecci.ucr.ac.cr',2, '2021-01-01', '2021-11-09'),
    ('cristian.martinezhernandez@ucr.ac.cr', 3, '2013-01-01', '2021-11-01'),
    ('donny.nunez@ecci.ucr.ac.cr', 2, '2015-01-01', '2018-08-08'),
    ('andrea.alvaradoacon@ucr.ac.cr', 2, '2021-01-01', '2021-11-08'),
    ('GREIVIN.SANCHEZGARITA@ucr.ac.cr', 3, '2020-01-01', '2021-08-11'),
    ('SEBASTIAN.MONTEROCASTRO@ucr.ac.cr', 3, '2021-01-01', '2021-07-11'),
    ('DYLAN.ARIAS@ucr.ac.cr', 2, '2021-01-01', '2021-11-11'),
    ('CARLOS.MORAMEMBRENO@ucr.ac.cr', 3, '2021-01-01', '2021-07-11'),
     ('edgar.casasola@ucr.ac.cr', 2, '2021-01-01', '2021-11-07'),
    ('gabriela.marin@ucr.ac.cr', 3, '2021-01-01', '2021-11-07'),
    ('jeisson.hidalgo@ucr.ac.cr', 2, '2021-01-01', '2021-11-11'),
    ('joseantonio.brenes@ucr.ac.cr', 3, '2018-01-01', '2021-11-07'),  
    ('luis.esquivel@ucr.ac.cr', 3, '2013-01-01', '2021-11-07'),
    ('vladimir.lara@ucr.ac.cr', 3, '2003-01-01', '2019-11-11'),

    -- Agregadas para las nuevas publicaciones
	('marta.calderon@ecci.ucr.ac.cr',2, '2015-01-01', '2018-08-08'),
	('lyon.villalobos@gmail.com', 2, '2015-01-01', '2018-08-08'),
	('denisse.gmadrigal@gmail.com', 3, '2015-01-01', '2018-08-08'),
	('brenda.aymerich@ucr.ac.cr',2, '2015-01-01', '2018-08-08'),
	('IGNACIO.DIAZOREIRO@ucr.ac.cr', 2, '2015-01-01', '2018-08-08'),
	('julio.guzman@ucr.ac.cr',3, '2015-01-01', '2018-08-08'),
	('diana.garbanzo@ucr.ac.cr',2, '2015-01-01', '2018-08-08'),
	('carlos.castro@ecci.ucr.ac.cr',3, '2015-01-01', '2018-08-08'),
	('ruizble@yahoo.com',3, '2015-01-01', '2018-08-08'),
	('juan.fonsecasolis@ucr.ac.cr',2, '2015-01-01', '2018-08-08')
    ) 
AS Source ([CollaboratorEmail], [InvestigationGroupId], [StartDate],[EndDate]) ON Target.[CollaboratorEmail] = Source.[CollaboratorEmail] and Target.[InvestigationGroupId] = Source.[InvestigationGroupId]
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([CollaboratorEmail], [InvestigationGroupId], [StartDate],[EndDate]) VALUES ([CollaboratorEmail], [InvestigationGroupId], [StartDate],[EndDate]);

MERGE INTO [dbo].[Publication] AS Target 
USING (VALUES 
     ('10.1109/CLEI52000.2020.00068','Measuring students contributions in software development projects using git metrics','Many courses in the software engineering area are centered around team-based project development. Evaluating these projects is a challenge due to the difficulty of measuring individual student contributions versus team contributions. The adoption of distributed version control systems like Git enables the measurement of students’ and teams’ contributions to the project. In this work, we analyze the contributions within five software development projects from undergraduate courses that used project-based learning. For this, we generate visualizations of aggregated Git metrics using inequality indexes and inter-decile ratios, which offer insights into the practices and processes followed by students and teams throughout the project development. This approach allowed us to identify both inequality among students’ contributions and development processes with a non-steady pace, rendering a useful feedback tool for instructors and students during the development of the project. Further studies can be conducted to assess the complexity and value of students’ contributions by analyzing their source code commits and other software artifacts.','2020-10-23','Conference','2020 XLVI Latin American Computing Conference (CLEI)',1,'img/publicationPictures/CLEI2020.jpg',0, null, null), 
	('10.1109/CLEI52000.2020.00067','Online Judge Support for Programming Teaching','Online programming judges are considered useful and sometimes indispensable tools to support competitive programming, professionals’ recruiting, and programming education. In this last field, the scientific literature on these tools focuses on the learners’ needs, but neglects the requirements of the professors, even though they are who mainly decide whether or not an educational tool is adopted in the courses they teach. This article collected 132 functional requirements for educative online judges, from the scientific literature and programming teachers with experience in the use of this type of tool. To know the degree of support, the requirements were grouped into 27 categories, and a requirements verification was performed with four available educative online judges reported in a recent systematic literature review. A low degree of satisfaction of requirements was found. This result encourages future research to create tools that better support teaching-learning processes and the requirements collected are a useful contribution as a starting point for such research.','2020-10-23','Conference','2020 XLVI Latin American Computing Conference (CLEI)',1,'img/publicationPictures/CLEI2020.jpg',0, null, null),
	('10.1145/3416508.3417121','Evaluating hyper-parameter tuning using random search in support vector machines for software effort estimation','Studies in software effort estimation (SEE) have explored the use of hyper-parameter tuning for machine learning algorithms (MLA) to improve the accuracy of effort estimates. In other contexts random search (RS) has shown similar results to grid search, while being less computationally-expensive. In this paper, we investigate to what extent the random search hyper-parameter tuning approach affects the accuracy and stability of support vector regression (SVR) in SEE. Results were compared to those obtained from ridge regression models and grid search-tuned models. A case study with four data sets extracted from the ISBSG 2018 repository shows that random search exhibits similar performance to grid search, rendering it an attractive alternative technique for hyper-parameter tuning. RS-tuned SVR achieved an increase of 0.227 standardized accuracy (SA) with respect to default hyper-parameters. In addition, random search improved prediction stability of SVR models to a minimum ratio of 0.840. The analysis showed that RS-tuned SVR attained performance equivalent to GS-tuned SVR. Future work includes extending this research to cover other hyper-parameter tuning approaches and machine learning algorithms, as well as using additional data sets.','2020-11-08','Conference','Proceedings of the 16th ACM International Conference on Predictive Models and Data Analytics in Software Engineering',1,'img/publicationPictures/PROMISE2020.jpg',0, null, null),
    ('10.1007/978-3-030-51517-1_1','Alzheimers disease early detection using a low cost three-dimensional densenet-121 architecture','The objective of this work is to detect Alzheimer’s disease using Magnetic Resonance Imaging. For this, we use a three-dimensional densenet-121 architecture. With the use of only freely available tools, we obtain good results: a deep neural network showing metrics of 87% accuracy, 87% sensitivity (micro-average), 88% specificity (micro-average), and 92% AUROC (micro-average) for the task of classifying five different classes (disease stages). The use of tools available for free means that this work can be replicated in developing countries.','2020-06-23','Conference','International Conference on Smart Homes and Health Telematics. ICOST 2020: The Impact of Digital Technologies on Public Health in Developed and Developing Countries ',1,'img/publicationPictures/ICOST.jpg',0, null, null),	
	('10.23919/CISTI49556.2020.9141107','Assessing two graph-based algorithms in a modelbased testing platform for java applications','Model-based testing (MBT) is an approach for automatically generating test cases from a model of the system under test. Existing MBT tools support the automation of this process at varying degrees. One such tool is MBT4J, a research platform that extends ModelJUnit, offering a high level of automation. We extended MBT4J with two graph-based algorithms: the Chinese Postman Problem (CPP) and Breadth-First Search (BFS). The purpose of this study is to evaluate the efficacy of these two new algorithms added to MBT4J by comparing them to previous algorithms implemented in the platform. A case study was conducted using two open-source Java applications from public repositories, and twenty-one different configurations. The CPP tester performed similarly to previous testers in terms of time and coverage, and in addition, it resulted in a greater percentage of failed test cases in one application. The BFS tester was able to generate a greater amount of test cases when using fewer resources. We thus recommend using these algorithms for generating test cases for systems with complex models.','2020-06-27','Conference','2020 15th Iberian Conference on Information Systems and Technologies (CISTI)',1,'img/publicationPictures/CISTI2020.jpg',0, null, null),	
	('10.1007/978-3-030-40690-5_26','An empirical analysis of IFPUG FPA and COSMIC FFP measurement methods','The accuracy of functional size measuring is critical in software project management, because it is one of the key inputs for effort and cost estimation models. The functional size measurement (FSM) process is performed based on standardized methods; however, the accuracy of the FSM results is still based mostly on the knowledge of the measurers. In this paper, an empirical study was conducted to analyze the accuracy, reproducibility, and acceptance properties of the IFPUG FPA and COSMIC FFP functional size measurement methods. Results show that the performance of participants in measuring the requirement specifications using IFPUG FPA and COSMIC FFP did not differ significantly in terms of accuracy and reproducibility. Likewise, acceptance properties such as perceived ease of use, perceived usefulness, and intention to use did not present significant differences. Our results suggest that novice measurers could apply both methods with similar results.','2020-01-31','Conference','Advances in Intelligent Systems and Computing.',1,'img/publicationPictures/AISC.jpg',0, null, null),
	('10.1007/978-3-030-01171-0_15','MBT4J: automating the model-based testing process for java applications','Model-based testing is a process that can reduce the cost of software testing by automating the design and generation of test cases but it usually involves some time-consuming manual steps. Current model-based testing tools automate the generation of test cases, but offer limited support for the model creation and test execution stages. In this paper we present MBT4J, a platform that automates most of the model-based testing process for Java applications, by integrating several existing tools and techniques. It automates the model building, test case generation, and test execution stages of the process. First, a model is extracted from the source code, then an adapter—between this model and the software under test—is generated and finally, test cases are generated and executed. We performed an evaluation of our platform with 12 configurations using an existing Java application from a public repository. Empirical results show that MBT4J is able to generate up to 2,438 test cases, detect up to 289 defects, and achieve a code coverage ranging between 72% and 84%. In the future, we plan to expand our evaluation to include more software applications and perform error seeding in order to be able to analyze the false positive and negative rates of our platform. Improving the automation of oracles is another vein for future research.','2018-09-27','Conference','International Conference on Software Process Improvement. CIMPS 2018: Trends and Applications in Software Engineering.',1,'img/publicationPictures/CIMPS2018.jpg',0, null, null),	
	('10.1145/3183377.3183397','Use of JiTT in a graduate software testing course: an experience report','This paper describes our experience using Just-in-Time Teaching (JiTT) in a graduate Software Testing course during two semesters. JiTT is a pedagogical strategy that bridges in-class and out-of-class components through preparatory web-based assignments, known as warm-ups. Our JiTT design was as follows. The preparatory out-of-class component consisted of a reading test, which required students to read a chapter from the textbook and then answer a web-based test available in our virtual platform. Reading tests were due the day before class in order to give the teacher enough time to read over the student’s responses and adjust the next lesson accordingly. The in-class component was organized around student common misconceptions or difficulties, extracted from the reading tests submitted by the students. Discussions and cooperative learning activities were among the teaching strategies used in class. Our approach was assessed from the students’ and teacher’s perspective. The students’ perspective was obtained from a survey. The teacher’s perspective consisted in an assessment of strengths and limitations. Results from our evaluation show that a vast majority of students believe their learning improves when they prepare for class by reading the material in advance. They also think that reading tests are an effective way of verifying that students did the assigned reading. Most of them also consider that JiTT is an appropriate teaching strategy for the course. From the teacher’s perspective, a major strength found was that students were more engaged in class, asking interesting questions that enriched class discussion. Also, the use of open-ended (essay-type) questions in reading tests has the additional benefit of helping them become better writers (organize their ideas better and clarify their thinking through writing).','2018-05-27','Conference','ICSE-SEET ’18: Proceedings of the 40th International Conference on Software Engineering: Software Engineering Education and Training',1,'img/publicationPictures/ICSE2018.jpg',0, null, null),
	('10.1007/978-3-319-94229-2_13','Software Development Practices in Costa Rica: A Survey','In recent years, many studies have focused on software development practices around the world. The HELENA study is an international effort to gather quantitative data on software development practices and frameworks. In this paper, we present the Costa Rican results of the HELENA survey. We provide evidence of the practices and frameworks used in 51 different projects in Costa Rica. Participants in this survey represent companies ranging from 50 or fewer employees to companies with more than 2500 employees. Furthermore, the industries represented in the survey include software development, system development, IT consulting, research and development of IT services and software development for financial institutions. Results show that Scrum, Iterative Development, Kanban and Waterfall are the most used software development frameworks in Costa Rica. However, Scrum doubles the use of Waterfall and other methods.','2018-06-29','Journal','International Conference on Applied Human Factors and Ergonomics. AHFE 2018: Advances in Artificial Intelligence, Software and Systems Engineering ',1,'img/publicationPictures/AHFE2018.jpg',0, null, null),	
	('10.1186/s40411-017-0037-x', 'A genetic algorithm based framework for software effort prediction','Several prediction models have been proposed in the literature using different techniques obtaining different results in different contexts. The need for accurate effort predictions for projects is one of the most critical and complex issues in the software industry. The automated selection and the combination of techniques in alternative ways could improve the overall accuracy of the prediction models.' ,'2016-05-31','Journal','Journal of Software Engineering Research and Development',1,'img/publicationPictures/JSERD.jpg',0, null, null),	
	('10.1121/1.4988827', 'Automatic recognition of accessible pedestrian signals','Accessible pedestrian signals (APS) enhance accessibility in streets around the world. Recent attempts to extend the use of APS to people with visual and audible impairments have emerged from the area of audio signal processing. Even though few authors have studied the recognition of APS by sound, comprehensive literature in Biology have been published for recognizing other simple sounds like bird and frog calls. Since these calls exhibit the same periodic and modulated nature as APS, many of the existent approaches can be adapted for this purpose. We present an algorithm that uses the mentioned approach. The algorithm was evaluated using a collection of 79 recordings gathered from streets in San José, Costa Rica, where the solution will be implemented. Three types of sounds are available: a low-pitch chirp, a high-pitch chirp and, a cuckoo-like. The results showed a precision of 87%, a specificity of 83%, a sensibility of 86%, and a F-measure of 85%.','2017-06-10','Journal','The Journal of the Acoustical Society of America 141, 3913 (2017)',1,'img/publicationPictures/JASA2017.jpg',0, null, null),
	('10.1145/2998181.2998281', 'Awareness Supporting Technologies used in Collaborative Systems: A Systematic Literature Review','Since the establishment of Computer Supported Collaborative Work as a research area, computer advances have change the paradigm of how technology is applied to improve the performance in collaborative scenarios. Notifications are an important part of this improvement. Technological systems have been applied in order to provide collaborators with the sufficient awareness to keep a task going. In this paper we present the protocol and results of a Systematic Literature Review that delves in the application of new technologies to provide awareness in collaborative systems. Moreover, we classify the collaborative systems found in literature using two traditional taxonomies for CSCW in order to understand which notification mechanisms are used to support which systems. Our review covers the last 10 years and classifies system prototypes based on the context in which they are applied, the notification and information gathering mechanism used, and the assessment performed. With over 400 papers reviewed, 83 that met the review requirements were included. The review results show that traditional interfaces and mobile devices are still the most common notification mechanisms. However, ubiquitous devices and non-traditional interfaces have also been used.','2017-02-25','Conference','CSCW ’17: Proceedings of the 2017 ACM Conference on Computer Supported Cooperative Work and Social Computing',1,'img/publicationPictures/CSCW107.jpg',0, null, null),	
	('10.1007/s12652-017-0475-7', 'Automatic recognition of the American sign language fingerspelling alphabet to assist people living with speech or hearing impairments','Sign languages are natural languages used mostly by deaf and hard of hearing people. Different development opportunities for people with these disabilities are limited because of communication problems. The advances in technology to recognize signs and gestures will make computer supported interpretation of sign languages possible. There are more than 137 different sign languages around the world; therefore, a system that interprets them could be beneficial to all, especially to the Deaf Community. This paper presents a system based on hand tracking devices (Leap Motion and Intel RealSense), used for signs recognition. The system uses a Support Vector Machine for sign classification. Different evaluations of the system were performed with over 50 individuals; and remarkable recognition accuracy was achieved with selected signs (100% accuracy was achieved recognizing some signs). Furthermore, an exploration on the Leap Motion and the Intel RealSense potential as a hand tracking devices for sign language recognition using the American Sign Language fingerspelling alphabet was performed.','2017-03-22','Journal','Journal of Ambient Intelligence and Humanized Computing',1,'img/publicationPictures/JAI&HC.jpg',0, null, null), 
	('10.1007/s12652-016-0438-4','Human aspects of ubiquitous computing: a study addressing willingness to use it and privacy issues','Identifying the human aspects related to ubiquitous systems focused on people’s willingness to use them and privacy concerns was our goal. We selected two ubiquitous systems: a wearable system (Google Glass) and an embedded in context system (Smart Environments). An online survey, with more than 400 participants, which included questions about how people perceive privacy issues related to the use of these two different ubiquitous systems, was conducted. Results show that privacy is not the only factor defining predisposition or aversion towards using ubiquitous systems. Financial, risk, and convenience factors are the others. We discovered that the importance of these factors on the decision to use them or not depends on the system. Regarding privacy, Google Glass generates a higher degree of concern than the Smart Environments alternative. Female participants tend to be more worried than male participants, independently of the ubiquitous system considered. Finally, the youngest participants (16–25 years old) are the most concerned about privacy threats, which was unexpected.','2016-12-26','Journal','Journal of Ambient Intelligence and Humanized Computing',2,'img/publicationPictures/JAI&HC.jpg',0, null, null),	
	('10.1109/FIE.2016.7757692','Learning principles in program visualizations: A systematic literature review','Program visualizations help students understand the runtime behavior of other programs. They are educational tools to complement lectures or replace inefficient static drawings. A recent survey found 46 program visualizations developed from 1979 to 2012 reported that their effectiveness is unclear. They also evaluated learner engagement strategies implemented by visualization systems, but other learning principles were not considered. Learning principles are potential key factors in the success of program visualization as learning tools. In this paper, we identified 16 principles that may contribute to the effectiveness of a learning tool based on Vygotsky’s learning theory. We hypothesize that some of these principles could be supported by incorporating visual concrete allegories and gamification. We conducted a literature review to know if these principles are supported by existing solutions. We found six new systems between 2012 and 2015. Very few systems consider a learning theory as theoretical framework. Only two out the 16 learning principles are supported by existing visualizations. All systems use unconnected visual metaphors, two use concrete visual metaphors, and one implemented a gamification principle. We expect that using concrete visual allegories and gamification in future program visualizations will significantly improve their effectiveness.','2016-10-15','Conference',' 2016 IEEE Frontiers in Education Conference (FIE)',2,'img/publicationPictures/FIE2016.jpg',0, null, null),
    ('10.1007/978-3-319-67585-5_2', 'Smart Cities in Latin America', 'In almost every forum around the world, country leaders are discussing the necessity of creating smart cities. However, even the term "smart city" is diffuse nowadays. Some countries want their cities to become smarter and others want to create smart cities from scratch. Several mappings have been developed around the world to locate the smartest cities. We believe that, since Latin American and Caribbean countries are exploring in the creation of smart cities, a proper mapping and plan is necessary to assure that the efforts in creating smart cities are not a waste. Using a literature review and a survey, we try to determine the state of smart cities development and its technical readiness in the Region.', '2016-08-23', 'Conference', 'Ubiquitous Computing and Ambient Intelligence', 1, 'img/publicationPictures/UCAI2017.jpg', 0, null, null) 
     ) 
AS Source ([Id], [Name], [Summary] , [Year] , [TypePublication],[JournalConference],[ResearchGroupId],[Image],[Deleted],[DocumentPDF],[DocumentPDFAttached]) ON Target.[Id] = Source.[Id] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Id], [Name], [Summary] , [Year] , [TypePublication],[JournalConference],[ResearchGroupId],[Image],[Deleted],[DocumentPDF],[DocumentPDFAttached]) 
VALUES ([Id], [Name], [Summary] , [Year] , [TypePublication],[JournalConference],[ResearchGroupId],[Image],[Deleted],[DocumentPDF],[DocumentPDFAttached]);


MERGE INTO [dbo].[CollaboratorIsAuthorOfPublication] AS Target 
 USING (VALUES 
('sivana.hamercampos@ucr.ac.cr','10.1109/CLEI52000.2020.00068'), 
 ('cristian.quesadalopez@ucr.ac.cr','10.1109/CLEI52000.2020.00068'), 
 ('alexandra.martinez@ecci.ucr.ac.cr','10.1109/CLEI52000.2020.00068'), 
 ('marcelo.jenkings@ucr.ac.cr','10.1109/CLEI52000.2020.00068'), 
 ('jeisson.hidalgo@ucr.ac.cr','10.1109/CLEI52000.2020.00067' ),
 ('gabriela.marin@ucr.ac.cr','10.1109/CLEI52000.2020.00067' ),
 ('marta.calderon@ecci.ucr.ac.cr','10.1109/CLEI52000.2020.00067' ),
 ('lyon.villalobos@gmail.com','10.1145/3416508.3417121'),
 ('cristian.quesadalopez@ucr.ac.cr','10.1145/3416508.3417121'),
 ('jose.guevaracoto@ucr.ac.cr','10.1145/3416508.3417121'),
 ('alexandra.martinez@ecci.ucr.ac.cr','10.1145/3416508.3417121'),
 ('marcelo.jenkings@ucr.ac.cr','10.1145/3416508.3417121'),
 ('braulio.solano@ecci.ucr.ac.cr','10.1007/978-3-030-51517-1_1'),
 ('ricardo.villalon@ucr.ac.cr','10.1007/978-3-030-51517-1_1'),
 ('gabriela.marin@ucr.ac.cr','10.1007/978-3-030-51517-1_1'),
 ('alexandra.martinez@ecci.ucr.ac.cr','10.23919/CISTI49556.2020.9141107'),
 ('cristian.quesadalopez@ucr.ac.cr','10.23919/CISTI49556.2020.9141107'),
 ('marcelo.jenkings@ucr.ac.cr','10.23919/CISTI49556.2020.9141107'),
 ('lyon.villalobos@gmail.com','10.23919/CISTI49556.2020.9141107'),
 ('denisse.gmadrigal@gmail.com','10.1007/978-3-030-40690-5_26'),
 ('cristian.quesadalopez@ucr.ac.cr','10.1007/978-3-030-40690-5_26'),
 ('marcelo.jenkings@ucr.ac.cr','10.1007/978-3-030-40690-5_26'), 
 ('lyon.villalobos@gmail.com','10.1007/978-3-030-01171-0_15'),
 ('cristian.quesadalopez@ucr.ac.cr','10.1007/978-3-030-01171-0_15'),
 ('alexandra.martinez@ecci.ucr.ac.cr','10.1007/978-3-030-01171-0_15'),
 ('marcelo.jenkings@ucr.ac.cr','10.1007/978-3-030-01171-0_15'),
 ('alexandra.martinez@ecci.ucr.ac.cr','10.1145/3183377.3183397'),
 ('brenda.aymerich@ucr.ac.cr','10.1007/978-3-319-94229-2_13'),
 ('IGNACIO.DIAZOREIRO@ucr.ac.cr','10.1007/978-3-319-94229-2_13'),
 ('julio.guzman@ucr.ac.cr','10.1007/978-3-319-94229-2_13'),
 ('gustavo.lopez_h@ucr.ac.cr','10.1007/978-3-319-94229-2_13'),
 ('diana.garbanzo@ucr.ac.cr','10.1007/978-3-319-94229-2_13'),
 ('juan.murillomorera@ucr.ac.cr','10.1186/s40411-017-0037-x'),
 ('cristian.quesadalopez@ucr.ac.cr','10.1186/s40411-017-0037-x'),
 ('carlos.castro@ecci.ucr.ac.cr','10.1186/s40411-017-0037-x'),
 ('marcelo.jenkings@ucr.ac.cr','10.1186/s40411-017-0037-x'), 
 ('arturo.camacho@ecci.ucr.ac.cr','10.1121/1.4988827'),
 ('ruizble@yahoo.com','10.1121/1.4988827'),
 ('juan.fonsecasolis@ucr.ac.cr','10.1121/1.4988827'),
 ('gustavo.lopez_h@ucr.ac.cr','10.1145/2998181.2998281'),
 ('luis.guerrero@ecci.ucr.ac.cr','10.1145/2998181.2998281'),
 ('luis.quesada@ecci.ucr.ac.cr','10.1007/s12652-017-0475-7'),
 ('gustavo.lopez_h@ucr.ac.cr','10.1007/s12652-017-0475-7'),
 ('luis.guerrero@ecci.ucr.ac.cr','10.1007/s12652-017-0475-7'),
 ('gustavo.lopez_h@ucr.ac.cr','10.1007/s12652-016-0438-4'),
 ('gabriela.marin@ucr.ac.cr','10.1007/s12652-016-0438-4'),
 ('marta.calderon@ecci.ucr.ac.cr','10.1007/s12652-016-0438-4'),
 ('jeisson.hidalgo@ucr.ac.cr','10.1109/FIE.2016.7757692'),
 ('gabriela.marin@ucr.ac.cr','10.1109/FIE.2016.7757692'),
 ('vladimir.lara@ucr.ac.cr','10.1109/FIE.2016.7757692')

 )
 AS Source ([EmailCollaborator], [IdPublication]) ON Target.[EmailCollaborator] = Source.[EmailCollaborator] and Target.[IdPublication] = Source.[IdPublication]
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([EmailCollaborator], [IdPublication]) 
VALUES ([EmailCollaborator], [IdPublication]);

/*
MERGE INTO Thesis AS Target 
USING (VALUES 
    --('correo', 'nombre','primerApellido', 'segundoApellido'), 
    (1,'Tesis 1', '2015-01-01', 'Resumen', 1, null, 'DOI','Type'),
    (2,'Tesis 2', '2015-01-01', 'Resumen', 1, null, 'DOI','Type')

    ) 
AS Source ([Id],[Name],[PublicationDate],[Summary],[InvestigationGroupId],[Image],[DOI],[Type]) ON Target.[Id] = Source.[Id] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Id],[Name],[PublicationDate],[Summary],[InvestigationGroupId],[Image],[DOI],[Type]) VALUES ([Id],[Name],[PublicationDate],[Summary],[InvestigationGroupId],[Image],[DOI],[Type]);
*/

/*
MERGE INTO CollaboratorPartOfThesis AS Target 
USING (VALUES 
    --('correo', 'nombre','primerApellido', 'segundoApellido'), 
    ('arturo.camacho@ecci.ucr.ac.cr', 1),
    ('adrvegve@gmail.com',1),
    ('andrea.chaconpaez@ucr.ac.cr', 2),
    ('braulio.solano@ecci.ucr.ac.cr',1)
    ) 
AS Source ([CollaboratorEmail], [ThesisId]) ON Target.[CollaboratorEmail] = Source.[CollaboratorEmail] and Target.[ThesisId] = Source.[ThesisId]
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([CollaboratorEmail], [ThesisId]) VALUES ([CollaboratorEmail], [ThesisId]);*/



MERGE INTO [dbo].[ResearchAreaPublication] AS TARGET
USING
(VALUES
(6, '10.1109/CLEI52000.2020.00068'),
(7, '10.1109/CLEI52000.2020.00068'),
(23, '10.1109/CLEI52000.2020.00068'),
(23, '10.1109/CLEI52000.2020.00067'),
(15, '10.1145/3416508.3417121'),
(6, '10.1145/3416508.3417121'),
(7, '10.1145/3416508.3417121'),
(23, '10.1145/3416508.3417121'),
(7, '10.1007/978-3-030-51517-1_1'),
(20, '10.23919/CISTI49556.2020.9141107'),
(21, '10.23919/CISTI49556.2020.9141107'),
(23, '10.23919/CISTI49556.2020.9141107'),
(15, '10.1007/978-3-030-40690-5_26'),
(23, '10.1007/978-3-030-40690-5_26'),
(20, '10.1007/978-3-030-01171-0_15'),
(21, '10.1007/978-3-030-01171-0_15'),
(23, '10.1007/978-3-030-01171-0_15'),
(4, '10.1145/3183377.3183397'),
(23, '10.1007/978-3-319-94229-2_13'),
(15, '10.1186/s40411-017-0037-x'),
(6, '10.1186/s40411-017-0037-x'),
(7, '10.1186/s40411-017-0037-x'),
(23, '10.1186/s40411-017-0037-x'),
(5, '10.1121/1.4988827'),--Dudoso
(23, '10.1145/2998181.2998281'),--Dudoso
(6, '10.1007/s12652-017-0475-7'),
(22, '10.1007/s12652-017-0475-7'),
(6, '10.1007/s12652-016-0438-4'),
(22, '10.1007/s12652-016-0438-4'),
(11, '10.1007/s12652-016-0438-4'),
(4, '10.1109/FIE.2016.7757692'),
(23, '10.1007/978-3-319-67585-5_2')
)
AS SOURCE ([ResearchAreasId], [PublicationsId]) ON TARGET.[ResearchAreasId] = SOURCE.[ResearchAreasId] AND TARGET.[PublicationsId] = SOURCE.[PublicationsId]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([ResearchAreasId], [PublicationsId])
VALUES ([ResearchAreasId], [PublicationsId]);

MERGE INTO ReferenceListPublication AS Target 
USING (VALUES 
	('10.1109/CLEI52000.2020.00068','G. Matturro, "Soft skills in software engineering: A study of its demand by software companies in uruguay", 2013 6th international workshop on cooperative and human aspects of software engineering (CHASE), pp. 133-136, 2013.',1),
	('10.1109/CLEI52000.2020.00068','V. Garousi, G. Giray, E. Tuzun, C. Catal and M. Felderer, "Closing the gap between software engineering education and industrial needs", IEEE Software, 2019.',2),
	('10.1109/CLEI52000.2020.00068','C. Quesada-López and A. Martínez, "Implementation of project based learning: Lessons learned", 2019 XLV Latin American Computing Conference (CLEI), pp. 1-10, 2019.',3),
	('10.1109/CLEI52000.2020.00068','J. E. Sims-Knight, R. L. Upchurch, T. Powers, S. Haden and R. Topciu, "Teams in software engineering education", 32nd Annual Frontiers in Education, vol. 3, pp. S3G-S3G, 2002.',4),
	('10.1109/CLEI52000.2020.00068','R. F. Gamble and M. L. Hale, "Assessing individual performance in agile undergraduate software engineering teams", 2013 IEEE Frontiers in Education Conference (FIE), pp. 1678-1684, 2013.',5),
	('10.1109/CLEI52000.2020.00068','F. Lanubile, C. Ebert, R. Prikladnicki and A. Vizcaíno, "Collaboration tools for global software engineering", IEEE software, vol. 27, no. 2, pp. 52-55, 2010.',6),
	('10.1109/CLEI52000.2020.00068','S. Chacon and B. Straub, Pro git, 2014, [online] Available: .',7),
	('10.1109/CLEI52000.2020.00068','R. M. Parizi, P. Spoletini and A. Singh, "Measuring team members contributions in software engineering projects using git-driven technology", 2018 IEEE Frontiers in Education Conference (FIE), pp. 1-5, 2018.',8),
	('10.1109/CLEI52000.2020.00068','J. Feliciano, M.-A. Storey and A. Zagalsky, "Student experiences using github in software engineering courses: a case study", 2016 IEEE/ACM 38th International Conference on Software Engineering Companion (ICSE-C), pp. 422-431, 2016.',9),
	('10.1109/CLEI52000.2020.00068','J. Kelleher, "Employing git in the classroom", 2014 World Congress on Computer Applications and Information Systems (WCCAIS), pp. 1-4, 2014.',10),
	('10.1109/CLEI52000.2020.00068','M. Cochez, V. Isomöttönen, V. Tirronen and J. Itkonen, "How do computer science students use distributed version control systems?", International Conference on Information and Communication Technologies in Education Research and Industrial Applications, pp. 210-228, 2013.',11),
	('10.1109/CLEI52000.2020.00068','M. Mittal and A. Sureka, "Process mining software repositories from student projects in an undergraduate software engineering course", Companion Proceedings of the 36th International Conference on Software Engineering, pp. 344-353, 2014.',12),
	('10.1109/CLEI52000.2020.00068','C.-Z. Kertész, "Using github in the classroom-a collaborative learning experience", 2015 IEEE 21st International Symposium for Design and Technology in Electronic Packaging (SIITME), pp. 381-386, 2015.',13),
	('10.1109/CLEI52000.2020.00068','M. Goeminne and T. Mens, "Evidence for the pareto principle in open source software activity", the Joint Porceedings of the 1st International workshop on Model Driven Software Maintenance and 5th International Workshop on Software Quality and Maintainability, pp. 74-82, 2011.',14),
	('10.1109/CLEI52000.2020.00068','K. Yamashita, S. McIntosh, Y. Kamei, A. E. Hassan and N. Ubayashi, "Revisiting the applicability of the pareto principle to core development teams in open source software projects", Proceedings of the 14th International Workshop on Principles of Software Evolution, pp. 46-55, 2015.',15),
	('10.1109/CLEI52000.2020.00068','A. Agrawal, A. Rahman, R. Krishna, A. Sobran and T. Menzies, "We dont need another hero? the impact of “heroes” on software development", Proceedings of the 40th International Conference on Software Engineering: Software Engineering in Practice, pp. 245-253, 2018.',16),
	('10.1109/CLEI52000.2020.00068','A. Cobham, L. Schlögl and A. Sumner, "Inequality and the tails: the palma proposition and ratio", Global Policy, vol. 7, no. 1, pp. 25-36, 2016.',17),
	('10.1109/CLEI52000.2020.00068','V. Basili, J. Heidrich, M. Lindvall, J. Münch, M. Regardie, D. Rombach, et al., Gqm+ strategies: A comprehensive methodology for aligning business strategies with software measurement, 2014.',18),
	('10.1109/CLEI52000.2020.00068','F. A. Cowell, "Measurement of inequality", Handbook of income distribution, vol. 1, pp. 87-166, 2000.',19),
    ('10.1145/3416508.3417121','Amritanshu Agrawal, Wei Fu, Di Chen, Xipeng Shen, and Tim Menzies. 2019. How to" DODGE" Complex Software Analytics. IEEE Transactions on Software Engineering ( 2019 ).',1),
    ('10.1145/3416508.3417121','Chris Albon. 2018. Machine learning with python cookbook: Practical solutions from preprocessing to deep learning. " O Reilly Media, Inc.". ',2),
    ('10.1145/3416508.3417121','James Bergstra and Yoshua Bengio. 2012. Random search for hyper-parameter optimization. Journal of machine learning research 13, Feb ( 2012 ), 281-305.',3),
    ('10.1145/3416508.3417121','James S Bergstra, Rémi Bardenet, Yoshua Bengio, and Balázs Kégl. 2011. Algorithms for hyper-parameter optimization. In Advances in neural information processing systems. 2546-2554. ',4),
    ('10.1145/3416508.3417121','Michelle H Cartwright, Martin J Shepperd, and Qinbao Song. 2004. Dealing with missing software project data. In Proceedings. 5th International Workshop on Enterprise Networking and Computing in Healthcare Industry (IEEE Cat. No. 03EX717). IEEE, 154-165. ',5),
    ('10.1145/3416508.3417121','Jacob Cohen. 1992. A power primer. Psychological bulletin 112, 1 ( 1992 ), 155.',6),
    ('10.1145/3416508.3417121','Anna Corazza, Sergio Di Martino, Filomena Ferrucci, Carmine Gravino, Federica Sarro, and Emilia Mendes. 2010. How efective is tabu search to configure support vector regression for efort estimation?. In Proceedings of the 6th international conference on predictive models in software engineering. 1-10. ',7),
    ('10.1145/3416508.3417121','Anna Corazza, Sergio Di Martino, Filomena Ferrucci, Carmine Gravino, Federica Sarro, and Emilia Mendes. 2013. Using tabu search to configure support vector regression for efort estimation. Empirical Software Engineering 18, 3 ( 2013 ), 506-546.',8),
    ('10.1145/3416508.3417121','Karel Dejaeger, Wouter Verbeke, David Martens, and Bart Baesens. 2011. Data mining techniques for software efort estimation: a comparative study. IEEE transactions on software engineering 38, 2 ( 2011 ), 375-397. ',9),
    ('10.1145/3416508.3417121','Reiner Dumke and Alain Abran. 2016. COSMIC Function Points: Theory and Advanced Practices. CRC Press.',10),
    ('10.1007/978-3-030-51517-1_1','Alzheimer’s Disease Neuroimaging Initiative: Study Design (2017).',1),
    ('10.1007/978-3-030-51517-1_1','Bäckström, K., Nazari, M., Gu, I.Y., Jakola, A.S.: An efficient 3D deep convolutional network for Alzheimer’s disease diagnosis using MR images. In: 2018 IEEE 15th International Symposium on Biomedical Imaging (ISBI 2018), pp. 149–153, April 2018.',2),
    ('10.1007/978-3-030-51517-1_1','Carneiro, T., Medeiros Da NóBrega, R.V., Nepomuceno, T., Bian, G., De Albuquerque, V.H.C., Filho, P.P.R.: Performance analysis of Google colaboratory as a tool for accelerating deep learning applications. IEEE Access 6, 61677–61685 (2018).',3),
    ('10.1007/978-3-030-51517-1_1','Cheng, D., Liu, M.: CNNs based multi-modality classification for AD diagnosis. In: 2017 10th International Congress on Image and Signal Processing, BioMedical Engineering and Informatics (CISP-BMEI), pp. 1–5, October 2017.',4),
    ('10.1007/978-3-030-51517-1_1','Cohen, J.P., Bertin, P., Frappier, V.: Chester: A Web Delivered Locally Computed Chest X-Ray Disease Prediction System. arXiv:1901.11210 (2019)',5),
    ('10.1007/978-3-030-51517-1_1','Cui, R., Liu, M.: Hippocampus analysis by combination of 3-D DenseNet and shapes for Alzheimer’s disease diagnosis. IEEE J. Biomed. Health Inform. 23(5), 2099–2107 (2019).',6),
    ('10.1007/978-3-030-51517-1_1','Graber, M., Franklin, N.: Diagnostic error in internal medicine. Arch. Intern. Med. 165 (2005). ',7),
    ('10.1007/978-3-030-51517-1_1','Hara, K., Kataoka, H., Satoh, Y.: Can spatiotemporal 3D CNNs retrace the history of 2D CNNs and ImageNet? In: Proceedings of the IEEE Conference on Computer Vision and Pattern Recognition (CVPR), pp. 6546–6555 (2018)',8),
    ('10.1007/978-3-030-51517-1_1','He, G., Ping, A., Wang, X., Zhu, Y.: Alzheimer’s disease diagnosis model based on three-dimensional full convolutional DenseNet. In: 2019 10th International Conference on Information Technology in Medicine and Education (ITME), pp. 13–17, August 2019.',9),
    ('10.1007/978-3-030-51517-1_1','Huang, G., Liu, Z., Van Der Maaten, L., Weinberger, K.Q.: Densely connected convolutional networks. In: 2017 IEEE Conference on Computer Vision and Pattern Recognition (CVPR), Honolulu, HI, pp. 2261–2269 (2017).',10),
    ('10.23919/CISTI49556.2020.9141107','P. Bourque, R. E. Fairley et al., "Guide to the software engineering body ofknowledge (SWEBOK (R)): Version 3.0", (IEEE Computer Society Press, 2014.',1),
    ('10.23919/CISTI49556.2020.9141107','O. Taipale, J. Kasurinen, K. Karhu and K. Smolander, "Trade-off between auto-mated and manual software testing", International Journal of System Assurance Engineering and Management, vol. 2, no. 2, pp. 114-125, 2011.',2),
    ('10.23919/CISTI49556.2020.9141107','V. Garousi and F. Elberzhager, "Test automation: not just for test execution", IEEESoftware, vol. 34, no. 2, pp. 90-96, 2017.',3),
    ('10.23919/CISTI49556.2020.9141107','A. Rodrigues and A. Dias Neto, "Relevance and impact of critical factors of success insoftware test automation lifecycle: A survey", In: Proceedings of the 1st Brazilian Symposium on Systematic and Automated Software Testing, pp. 6, 2016.',4),
    ('10.23919/CISTI49556.2020.9141107','M. Utting and B. Legeard, Practical model-based testing: a tools approach, Morgan Kaufmann, 2010.',5),
    ('10.23919/CISTI49556.2020.9141107','M. Utting, B. Legeard, F. Bouquet, E. Fourneret, F. Peureux and A. Vernotte, "Recent advances in model-based testing", In: Advances in Computers, vol. 101, pp. 53-120, 2016.',6),
    ('10.23919/CISTI49556.2020.9141107','M. M. Mariano, É. F. Souza, A. T. Endo and N. L. Vijaykumar, "Analyzing graph-based algorithms employed to generate test cases from finite state machines", 2019 IEEE Latin American Test Symposium (LATS), pp. 1-6, 2019.',7),
    ('10.23919/CISTI49556.2020.9141107','L. Villalobos-Arias, C. Quesada-López, A. Martínez and M. Jenkins, "Model-based testing areas tools and challenges: A tertiary study", vol. 22, no. 1, 2019, [online] Available: https://doi.org/10.19153/cleiej.22.1.3.',8),
    ('10.23919/CISTI49556.2020.9141107','L. Villalobos-Arias, C. Quesada-López, A. Martínez and M. Jenkins, "MBT4J: Automating the Model-Based Testing Process for Java Applications", Trends and Applications in Software Engineering. CIMPS 2018. Advances in Intelligent Systems and Computing, vol. 865, [online] Available: https://doi.org/10.1007/978-3-030-01171-0\_15.',9),
    ('10.23919/CISTI49556.2020.9141107','L. Villalobos-Arias, C. Quesada-López, A. Martínez and M. Jenkins, "Evaluation of a model-based testing platform for Java applications", IET Software, 2019.',10),
    ('10.1007/978-3-030-40690-5_26','Abrahao, S.: On the functional size measurement of object-oriented conceptual schemas: design and evaluation issues. Universidad Politecnica de Valencia (2004)',1),
    ('10.1007/978-3-030-40690-5_26','Albrecht, A.: Measuring application development productivity. In: Proceedings of the Joint Share, Guide, and IBM Application Development Symposium (1979)',2),
    ('10.1007/978-3-030-40690-5_26','Bundschuh, M., Dekkers, C.: The IT Measurement Compendium: Estimating and Benchmarking Success with Functional Size Measurement. Springer, Heidelberg (2008)',3),
    ('10.1007/978-3-030-40690-5_26','COSMIC: The COSMIC Functional Size Measurement Method Version 4.0.1 Course Registration (C-REG) System Case Study. Version 2.0 (2015)',4),
    ('10.1007/978-3-030-40690-5_26','Davis, F.: User acceptance of information technology: system characteristics, user perceptions and behavioral impacts. J. Man Mach. 38(3), 475–487 (1993)',5),
    ('10.1007/978-3-030-40690-5_26','Fetcke, T.: The warehouse software portfolio: a case study in functional size measurement (1999)',6),
    ('10.1007/978-3-030-40690-5_26','ISO: Information Technology, Software Measurement, Functional Size Measurement: Definition of Concepts. ISO/IEC (2007)',7),
    ('10.1007/978-3-030-40690-5_26','Kemerer, C.: Reliability of function points measurement: a field experiment (1990)',8),
    ('10.1007/978-3-030-40690-5_26','Kemerer, C., Porter, B.: Improving the reliability of function point measurement: an empirical study. IEEE Trans. Software Eng. 18(11), 1011–1024 (1992)',9),
    ('10.1007/978-3-030-40690-5_26','Low, G., Jeffery, R.: Function points in the estimation and evaluation of the software process. IEEE Trans. Software Eng. 16(1), 64–71 (1990)',10),
    ('10.1007/978-3-030-01171-0_15','Utting, M., Legeard, B., Bouquet, F., Fourneret, E., Peureux, F., Vernotte, A.: Recent advances in model-based testing. In: Advances in Computers, vol. 101, pp. 53–120. Elsevier (2016)',1),
    ('10.1007/978-3-030-01171-0_15','Utting, M., Legeard, B.: Practical Model-Based Testing: A Tools Approach. Morgan Kaufmann, San Francisco (2010)',2),
    ('10.1007/978-3-030-01171-0_15','Pretschner, A., Prenninger, W., Wagner, S., Kühnel, C., Baumgartner, M., Sostawa, B., Zölch, R., Stauner, T.: One evaluation of model-based testing and its automation. In: Proceedings of the 27th International Conference on Software Engineering, pp. 392–401. ACM (2005)',3),
    ('10.1007/978-3-030-01171-0_15','Broy, M., Jonsson, B., Katoen, J.P., Leucker, M., Pretschner, A.: Model-based testing of reactive systems. In: LNCS, vol. 3472. Springer (2005)',4),
    ('10.1007/978-3-030-01171-0_15','Jard, C., Jeron, T.: TGV: theory, principles and algorithms. Int. J. Softw. Tools Technol. Transfer 7, 297–315 (2005)',5),
    ('10.1007/978-3-030-01171-0_15','SwissQ: Testing trends & benchmarks 2013. where do we stand where are we going to? (2013) https://swissq.it/wp-content/uploads/2016/02/Testing-Trends_und_Benchmarks2013.pdf',6),
    ('10.1007/978-3-030-01171-0_15','Schulze, C., Ganesan, D., Lindvall, M., Cleaveland, R., Goldman, D.: Assessing model-based testing: an empirical study conducted in industry. In: Proceedings of the 36th International Conference on Software Engineering. ACM (2014)',7),
    ('10.1007/978-3-030-01171-0_15','Ernits, J., Roo, R., Jacky, J., Veanes, M.: Model-based testing of web applications using NModel. In: Testing of Software Communication Systems. Springer (2009)',8),
    ('10.1007/978-3-030-01171-0_15','Pinheiro, A.C., Simao, A., Ambrosio, A.M.: FSM-based test case generation methods applied to test the communication software on board the ITASAT university satellite: a case study. J. Aerosp. Technol. Manag. 6, 447–461 (2014)',9),
    ('10.1007/978-3-030-01171-0_15','de Cleva Farto, G., Endo, A.T.: Evaluating the model-based testing approach in the context of mobile applications. Electron. Notes Theor. Comput. Sci. 314, 3–21 (2015)',10),
	('10.1145/3183377.3183397','T. Bailey and J. Forbes. 2005. Just-in-Time Teaching for CS0. In Proceedings of the 36th SIGCSE technical symposium on Computer science education. 366--370.',1),
	('10.1145/3183377.3183397','R. Blake, K. Marrs, J. Watt, and A. Gavrin. 2003. Just In Time Teaching (Jitt): Using The Web To Enhance Classroom Learning. In Proceedings of the American Society for Engineering Education Annual Conference and Exposition.',2),
	('10.1145/3183377.3183397','C.C. Bonwell and J.A. Edison. 1991. Active Learning: Creating Excitement in the Classroom. In ASHE ERIC Higher Education Report No. 1. The George Washington University, School of Education and Human Development.',3),
	('10.1145/3183377.3183397','C.C. Bonwell and J.A. Edison. 2010. Promoting active learning through case driven approach: An empirical study on database course. In StudentsâĂ&Zacute; Technology Symposium. 191--195.',4),
	('10.1145/3183377.3183397','T. Briggs. 2005. Techniques for Active Learning in CS Courses. 21 (December 2005), 156--165. ',5),
	('10.1145/3183377.3183397','P. Carter. 2009. An experiment with online instruction and active learning in an introductory computing course for engineers: JiTT meets CS1. In Proceedings of the 14th Western Canadian Conference on Computing Education. 103--108.',6),
	('10.1145/3183377.3183397','Paul Carter. 2012. An Experience Report: On the Use of Multimedia Pre-instruction and Just-in-time Teaching in a CS1 Course. In Proceedings of the 43rd ACM Technical Symposium on Computer Science Education. ACM, New York, NY, USA, 361--366.',7),
	('10.1145/3183377.3183397','D. Cordes and A. Parrish. 2002. Active learning in computer science: impacting student behavior. In Frontiers in Education, Vol. 1. T2A1--T2A5.',8),
	('10.1145/3183377.3183397','J. Davis. 2009. Experiences with Just-in-Time Teaching in Systems and Design Courses. In Proceedings of the 40th ACM Technical Symposium on Computer science education. 71--75.',9),
	('10.1145/3183377.3183397','Judith S. Gurka. 2012. JiTT in CS 1 and CS 2. J. Comput. Sci. Coll. 28, 2 (Dec. 2012), 81--86.',10),
	('10.1007/978-3-319-94229-2_13','Kuhrmann, M., Hanser, E., Prause, C.R., Diebold, P., Münch, J., Tell, P., Garousi, V., Felderer, M., Trektere, K., McCaffery, F., Linssen, O.: Hybrid software and system development in practice: waterfall, scrum, and beyond. In: Proceedings of the 2017 International Conference on Software and System Process - ICSSP 2017, pp. 30–39. ACM Press, New York (2017)',1),
	('10.1007/978-3-319-94229-2_13','CAMTIC: Camara de Tecnologias de Informacion y Comunicacion.',2),
	('10.1007/978-3-319-94229-2_13','West, D., Gilpin, M., Grant, T., Anderson, A.: Water-Scrum-Fall is the reality of agile for most organizations today (2011)',3),
	('10.1007/978-3-319-94229-2_13','Theocharis, G., Kuhrmann, M., Münch, J., Diebold, P.: Is water-scrum-fall reality? on the use of agile and traditional development practices. In: Abrahamsson, P., Corral, L., Oivo, M., Russo, B. (eds.) Product-Focused Software Process Improvement, pp. 149–166. Springer, Cham (2015)',4),
	('10.1007/978-3-319-94229-2_13','Paez, N., Fontdevila, D., Oliveros, A.: HELENA study: initial observations of software development practices in Argentina. In: Felderer, M., Méndez-Fernández, D., Turhan, B., Kalinowski, M., Sarro, F., Winkler, D. (eds.) Product-Focused Software Process Improvement, pp. 443–449. Springer, Cham (2017)',5),
	('10.1007/978-3-319-94229-2_13','Scott, E., Pfahl, D., Hebig, R., Heldal, R., Knauss, E.: Initial results of the HELENA survey conducted in Estonia with comparison to results from Sweden and worldwide. In: Felderer, M., Méndez-Fernández, D., Turhan, B., Kalinowski, M., Sarro, F., Winkler, D. (eds.) Product-Focused Software Process Improvement, pp. 404–412. Springer, Cham (2017)',6),
	('10.1007/978-3-319-94229-2_13','Nakatumba-Nabende, J., Kanagwa, B., Hebig, R., Heldal, R., Knauss, E.: Hybrid software and systems development in practice: perspectives from Sweden and Uganda. In: Felderer, M., Méndez-Fernández, D., Turhan, B., Kalinowski, M., Sarro, F., Winkler, D. (eds.) Product-Focused Software Process Improvement, pp. 413–419. Springer, Cham (2017)',7),
	('10.1007/978-3-319-94229-2_13','Tell, P., Pfeiffer, R.-H., Schultz, U.P.: HELENA stage 2—Danish overview. In: Felderer, M., Méndez-Fernández, D., Turhan, B., Kalinowski, M., Sarro, F., Winkler, D. (eds.) Product-Focused Software Process Improvement, pp. 420–427. Springer, Cham (2017)',8),
	('10.1007/978-3-319-94229-2_13','Felderer, M., Winkler, D., Biffl, S.: Hybrid software and system development in practice: initial results from Austria. In: Felderer, M., Méndez-Fernández, D., Turhan, B., Kalinowski, M., Sarro, F., Winkler, D. (eds.) Product Focused Software Process Improvement, pp. 435–442. Springer, Cham (2017)',9),
	('10.1186/s40411-017-0037-x','Albrecht, AJ (1979) Measuring application development productivity In: Proceedings of the Joint SHARE/GUIDE/IBM Application Development Symposium, vol 10, 83–92.. IBM Press.',1),
	('10.1186/s40411-017-0037-x','Aljahdali, S, Sheta A (2013) Evolving software effort estimation models using multigene symbolic regression genetic programming. Int J Adv Res Artif Intell 2: 52–57.',2),
	('10.1186/s40411-017-0037-x','Arcuri, A, Fraser G (2011) On parameter tuning in search based software engineering In: International Symposium on Search Based Software Engineering, 33–47.. Springer, Berling.',3),
	('10.1186/s40411-017-0037-x','Bala, A, Sharma AK (2015) A comparative study of modified crossover operators In: 2015 Third International Conference on Image Information Processing (ICIIP), 281–284.. IEEE, Waknaghat. doi:10.1109/ICIIP.2015.7414781.',4),
	('10.1186/s40411-017-0037-x','Becker, BG (1998) Visualizing decision table classifiers In: Information Visualization, 1998. Proceedings. IEEE Symposium On, 102–105.. IEEE, Research Triangle.',5),
	('10.1186/s40411-017-0037-x','Boehm, B (1981) Software Engineering Economics, Vol. 197. Prentice-hall Englewood Cliffs (NJ), USA.',6),
	('10.1186/s40411-017-0037-x','Chen, J, Nair V, Menzies T (2017) Beyond evolutionary algorithms for search-based software engineering. arXiv preprint arXiv:1701.07950.',7),
	('10.1186/s40411-017-0037-x','Dejaeger, K, Verbeke W, Martens D, Baesens B (2012) Data mining techniques for software effort estimation: a comparative study. IEEE Trans Softw Eng 38(2): 375–397.',8),
	('10.1186/s40411-017-0037-x','Dolado, JJ, Rodriguez D, Harman M, Langdon WB, Sarro F (2016) Evaluation of estimation models using the minimum interval of equivalence. Appl Soft Comput 49: 956–967. http://dx.doi.org/10.1016/j.asoc.2016.03.026.',9),
	('10.1186/s40411-017-0037-x','Ghatasheh, N, Faris H, Aljarah I, Al-Sayyed RM, et al. (2015) Optimizing software effort estimation models using firefly algorithm. J Softw Eng Appl 8(03): 133.',10),
	('10.1145/2998181.2998281','Hamed S. Alavi, Pierre Dillenbourg, and Frederic Kaplan. 2009. Distributed Awareness for Class Orchestration. In Learning in the Synergy of Multiple Disciplines, Ulrike Cress, Vania Dimitrova and Marcus Specht (eds.). Springer Berlin Heidelberg, 211--225. ',1),
	('10.1145/2998181.2998281','Pedro Antunes, Gustavo Zurita, and Nelson Baloian. 2009. A Model for Designing Geocollaborative Artifacts and Applications. In Groupware: Design, Implementation, and Use, Luís Carriço, Nelson Baloian and Benjamim Fonseca (eds.). Springer Berlin Heidelberg, Berlin, Heidelberg, 278--294.',2),
	('10.1145/2998181.2998281','Liliana Ardissono and Gianni Bosio. 2012. Context-dependent awareness support in open collaboration environments. User Modeling and User-Adapted Interaction 22, 3: 223--254.',3),
	('10.1145/2998181.2998281','Nikos Armenatzoglou, Yannis Marketakis, Lito Kriara, et al. 2009. FleXConf: A Flexible Conference Assistant Using Context-Aware Notification Services. In On the Move to Meaningful Internet Systems: OTM 2009 Workshops, Robert Meersman, Pilar Herrero and Tharam Dillon (eds.). Springer Berlin Heidelberg, 108--117.',4),
	('10.1145/2998181.2998281','Jakob Bardram, Jonathan Bunde-pedersen, Afsaneh Doryab, and Steffen Sørensen. 2009. CLINICAL SURFACES - Activity-Based Computing for Distributed Multi-Display Environments in Hospitals. In Human-Computer Interaction -- INTERACT 2009, Tom Gross, Jan Gulliksen, Paula Kotzé, et al. (eds.). Springer Berlin Heidelberg, 704--717.',5),
	('10.1007/s12652-017-0475-7','Caridakis G, Asteriadis S, Karpouzis K (2014) Non-manual cues in automatic sign language recognition. Pers Ubiquit Comput 18(1): 37–46. doi:10.1007/s00779-012-0615-1',1),
	('10.1007/s12652-017-0475-7','Carter M, Newn J, Velloso E, Vetere F (2015) Remote gaze and gesture tracking on the microsoft kinect. In: Proceedings of the Annual Meeting of the Australian Special Interest Group for Computer Human Interaction on - OzCHI’15 (pp 167–176). New York: ACM Press. doi:10.1145/2838739.2838778',2),
	('10.1007/s12652-017-0475-7','Chang CC, Lin CJ (2001) LIBSVM: a library for support vector machines. Retrieved from http://www.csie.ntu.edu.tw/?cjlin/libsvm',3),
	('10.1007/s12652-017-0475-7','Chuan CH, Regina E, Guardino C (2014) American sign language recognition using leap motion sensor. 2014 13th International Conference on Machine Learning and Applications, 541–544. doi:10.1109/ICMLA.2014.110',4),
	('10.1007/s12652-017-0475-7','Cortes C, Vapnik V (1995). Support-vector networks. Mach Learn, 20, 273–297. doi:10.1111/j.1747-0285.2009.00840.x',5),
	('10.1007/s12652-016-0438-4','Abascal J, Barbosa S, Fetter M et al (2015) Towards deeper understanding of user experience with ubiquitous computing systems: systematic literature review and design framework. In: Human–computer interaction. Springer International Publishing, Switzerland, pp 384–401',1),
	('10.1007/s12652-016-0438-4','Avizienis A, Laprie J, Randell B (2001) Fundamental concepts of dependability. Technical Report, University of Newcastle upon Tyne, Computing Science',2),
	('10.1007/s12652-016-0438-4','Baraldi L, Paci F, Serra G et al (2015) Gesture recognition using wearable vision sensors to enhance visitors’ museum experiences. IEEE Sens J 15:2705–2714. doi:10.1109/JSEN.2015.2411994',3),
	('10.1007/s12652-016-0438-4','Clarke R (2006) What’s privacy? In: Australian law reform commission workshop.',4),
	('10.1007/s12652-016-0438-4','De Silva LC, Morikawa C, Petra IM (2012) State of the art of smart homes. Eng Appl Artif Intell 25:1313–1321. doi:10.1016/j.engappai.2012.05.002',5),
	('10.1109/FIE.2016.7757692','J. Sorva, Visual Program Simulation in Introductory Programming Education, Aalto University, 2012.',1),
	('10.1109/FIE.2016.7757692','J. Sorva, V. Karavirta and L. Malmi, "A Review of Generic Program Visualization Systems for Introductory Programming Education", ACM Trans. Comput. Educ., vol. 13, no. 4, pp. 1-64, Nov. 2013.',2),
	('10.1109/FIE.2016.7757692','T. L. Naps, S. Rodger, J. A. Velázquez-Iturbide, G. Rößling, V. Almstrum, W. Dann, et al., "Exploring the role of visualization and engagement in computer science education", ACM SIGCSE Bull., vol. 35, no. 2, pp. 131, Jun. 2003.',3),
	('10.1109/FIE.2016.7757692','E. Isohanni and H.-M. Järvinen, "Are visualization tools used in programming education?: by whom how why and why not?", Proceedings of the 14th Koli Calling International Conference on Computing Education Research - Koli Calling 14, pp. 35-40, 2014.',4),
	('10.1109/FIE.2016.7757692','M. Hertz and M. Jump, "Trace-based teaching in early programming courses", Proceeding of the 44th ACM technical symposium on Computer scienceeducation - SIGCSE 13, pp. 561, 2013.',5)
	) 
AS Source ([IdPublication], [Reference], [Order]) ON Target.[IdPublication] = Source.[IdPublication] AND Target.[Reference] = Source.[Reference] AND Target.[Order] = Source.[Order]
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([IdPublication], [Reference], [Order] ) 
VALUES ([IdPublication], [Reference], [Order] );

MERGE INTO [dbo].[University] AS Target 
USING (VALUES 
    ('Universidad de Costa Rica'),
    ('Instituto Tecnológico de Costa Rica'),
    ('Universidad Nacional de Costa Rica'),
    ('Universidad Estatal de Carolina del Norte'),
    ('Universidad Politécnica de Valencia'),
    ('Universidad Federal de Rio de Janeiro'),
    ('Universidad Carolina Norte, Chapel Hill')
)
AS Source ([Name]) ON Target.[Name] = Source.[Name] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Name]) 
VALUES ([Name]);

MERGE INTO [dbo].[PersonsBelongsToUniversity] AS Target 
USING (VALUES 
    ('gustavoesquivel@ucr.ac.cr','Universidad de Costa Rica'),
    ('arturo.camacho@ecci.ucr.ac.cr','Universidad de Costa Rica'),
    ('davidviquez21@gmail.com','Universidad de Costa Rica'),
    ('adrvegve@gmail.com','Universidad de Costa Rica'),
    ('andrea.chaconpaez@ucr.ac.cr','Universidad de Costa Rica'),
    ('braulio.solano@ecci.ucr.ac.cr','Universidad de Costa Rica'),
    ('cristian.martinezhernandez@ucr.ac.cr','Universidad de Costa Rica'),
    ('donny.nunez@ecci.ucr.ac.cr','Universidad de Costa Rica'),
    ('andrea.alvaradoacon@ucr.ac.cr','Universidad de Costa Rica'),
    ('GREIVIN.SANCHEZGARITA@ucr.ac.cr','Universidad de Costa Rica'),
    ('SEBASTIAN.MONTEROCASTRO@ucr.ac.cr','Universidad de Costa Rica'),
    ('DYLAN.ARIAS@ucr.ac.cr','Universidad de Costa Rica'),
    ('CARLOS.MORAMEMBRENO@ucr.ac.cr','Universidad de Costa Rica'),
    ('edgar.casasola@ucr.ac.cr','Universidad de Costa Rica'),
    ('gabriela.barrantes@ucr.ac.cr','Universidad de Costa Rica'),
    ('gabriela.marin@ucr.ac.cr','Universidad de Costa Rica'),
    ('jeisson.hidalgo@ucr.ac.cr','Universidad de Costa Rica'),
    ('joseantonio.brenes@ucr.ac.cr','Universidad de Costa Rica'),
    ('kryscia.ramirez@ucr.ac.cr','Universidad de Costa Rica'),
    ('luis.esquivel@ucr.ac.cr','Universidad de Costa Rica'),
    ('ricardo.villalon@ucr.ac.cr','Universidad de Costa Rica'),
    ('vladimir.lara@ucr.ac.cr','Universidad de Costa Rica'),
    ('marcelo.jenkings@ucr.ac.cr','Universidad de Costa Rica'), 
    ('alexandra.martinez@ecci.ucr.ac.cr','Universidad de Costa Rica'),
    ('LEONARDO.VILLALOBOSARIAS@ucr.ac.cr','Universidad de Costa Rica'),
    ('sivana.hamercampos@ucr.ac.cr','Universidad de Costa Rica'),
    ('cristian.quesadalopez@ucr.ac.cr', 'Universidad de Costa Rica'), 
    ('ADRIAN.LARA@ucr.ac.cr', 'Universidad de Costa Rica'), 
    ('jose.guevaracoto@ucr.ac.cr','Universidad de Costa Rica'),
    ('abel.mendezporras@ucr.ac.cr','Instituto Tecnológico de Costa Rica'),
    ('juan.murillomorera@ucr.ac.cr','Universidad Nacional de Costa Rica'),
    ('lauriewilliams@gmail.com','Universidad Estatal de Carolina del Norte'),
    ('oscarpastorlopez@gmail.com','Universidad Politécnica de Valencia'),
    ('guilhermehortatracassos@gmail.com','Universidad Federal de Rio de Janeiro'),
    ('melissajensen@gmail.com','Universidad Carolina Norte, Chapel Hill'),
    ('erikahernandez@ucr.ac.cr','Universidad de Costa Rica'),
    ('pablo.ramirezmendez@ucr.ac.cr','Universidad de Costa Rica'),
    ('jose.mejiasrojas@ucr.ac.cr','Universidad de Costa Rica'),
    ('denisse.madrigalsanchez@ucr.ac.cr','Universidad de Costa Rica'),
    ('luis.salasvillalobos@ucr.ac.cr','Universidad de Costa Rica'),
    ('andres.martinezmesen@ucr.ac.cr','Universidad de Costa Rica'),
    ('rebeca.obandovazquez@ucr.ac.cr','Universidad de Costa Rica'),
    ('erik.kuhkmann@ucr.ac.cr','Universidad de Costa Rica'),
    ('juan.valverde@ucr.ac.cr','Universidad de Costa Rica'),
    ('antonio.badilla@ucr.ac.cr','Universidad de Costa Rica'),
    ('danel.salazar@ucr.ac.cr','Universidad de Costa Rica'),
    ('ricardo.franco@ucr.ac.cr','Universidad de Costa Rica'),
    ('stevenfernandez@ucr.ac.cr','Universidad de Costa Rica'),
    ('marta.calderon@ecci.ucr.ac.cr','Universidad de Costa Rica'),
	('lyon.villalobos@gmail.com','Universidad de Costa Rica'),
	('denisse.gmadrigal@gmail.com','Universidad de Costa Rica'),
	('brenda.aymerich@ucr.ac.cr','Universidad de Costa Rica'),
	('IGNACIO.DIAZOREIRO@ucr.ac.cr','Universidad de Costa Rica'),
	('julio.guzman@ucr.ac.cr','Universidad de Costa Rica'),
	('gustavo.lopez_h@ucr.ac.cr','Universidad de Costa Rica'),
	('diana.garbanzo@ucr.ac.cr','Universidad de Costa Rica'),
	('carlos.castro@ecci.ucr.ac.cr','Universidad de Costa Rica'),
	('ruizble@yahoo.com','Universidad de Costa Rica'),
	('juan.fonsecasolis@ucr.ac.cr','Universidad de Costa Rica'),
	('luis.guerrero@ecci.ucr.ac.cr','Universidad de Costa Rica'),
	('luis.quesada@ecci.ucr.ac.cr','Universidad de Costa Rica'))
AS Source ([PersonEmail], [UniversityName]) ON Target.[PersonEmail] = Source.[PersonEmail] AND Target.[UniversityName] = Source.[UniversityName] 
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([PersonEmail], [UniversityName]) 
VALUES ([PersonEmail], [UniversityName]);


MERGE INTO [dbo].[WorkWithUs] AS TARGET
USING 
(VALUES
    ('Trabaja con nosotros',
    'Al unirse a nuestro equipo de trabajo,
                   usted podrá desarrollarse en diversas areas de las tecnologias de la informacion como lo es
                   Ingeniería empirica, Interaccion Humano Computador, entre otras. Mas adelante se le mostrarán los pasos a seguir
                   para que forme parte de nosotros.','img/workWithUs.jpg','contacto@citic.ucr.ac.cr',
                   'Como requisitos previos se encuentra estar familiarizado con el entorno de las ciencias de computación y tener un
        gran interés a este. Además dependiendo del grupo de investigación al que desee unirse se solicitarán requisitos
        específicos acorde al área de interés.',
                    'Para los estudiantes de pregrado se requiere que se encuentren avanzados en la carrera de Ingeniera en computación, asi como
        presentar un registro de sus notas academicas a lo largo de su vida universitaria. Además presentar el horario del semestre
        en el que se encuentra para ajustar las horas de trabajo en el centro.',
                    'Para los estudiantes de posgrado se solicita que nos comente por qué desea trabajar en el centro y en que grupos de investigación tiene mayor interés.
        Además presentar una carta de recomendación por parte de algun profesor de la ECCI',
                    '  Para voluntarios se requiere que describa en que areas se ha desarrollado como estudiante o profesional y nos comente en que áreas del
        centro de investigación le gustaría colaborar. También indicar cuantas horas tiene disponible por semana para trabajar con nosotros en el
        sector que se asigne.'
                   )
   
)
AS SOURCE ([Name],[Description],[ImageName],[Email],[PreRequisites],[Pregrado],[Posgrado],[Voluntario]) ON TARGET.[Name] = SOURCE.[Name]
WHEN NOT MATCHED BY TARGET THEN
INSERT ([Name],[Description],[ImageName],[Email],[PreRequisites],[Pregrado],[Posgrado],[Voluntario]) VALUES ([Name],[Description],[ImageName],[Email],[PreRequisites],[Pregrado],[Posgrado],[Voluntario]);

