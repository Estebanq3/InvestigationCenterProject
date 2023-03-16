# El Sistema Grupos Investigación @UCR

## Tabla de contenidos

1. [Definiciones](#markdown-header-definiciones-acronimos-y-abreviaciones)

2. [Introducción](#markdown-header-introduccion)

3. [Equipos](#markdown-header-listado-de-equipos-y-miembros-de-los-equipos)

4. [Descripción del sistema](#markdown-header-descripcion-general)

    4.1 [Contexto y situación actual.](#markdown-header-contexto-actual)

    4.2 [Problema que resuelve.](#markdown-header-problema-que-resuelve)

    4.3 [Interesados del proyecto y tipos de usuarios.](#markdown-header-interesados-del-proyecto-y-tipos-de-usuarios)

    4.4 [Solución propuesta.](#markdown-header-solucion-propuesta)

    4.5 [Análisis del entorno.](#markdown-header-analisis-del-entorno)

    4.6 [Visión del producto.](#markdown-header-vision-del-producto)

    4.7 [Relación con otros sistemas externos.](#markdown-header-relacion-con-otros-sistemas-externos)

    4.8 [Descripción de los sistemas.](#markdown-header-descripcion-de-los-temas-modulos-asignados-a-cada-equipo)

    4.9 [Requerimientos funcionales.](#markdown-header-requerimientos-funcionales)

    4.10 [Mapa de ruta del producto.](#markdown-header-mapa-de-ruta-del-producto)

    4.11 [Requerimientos no funcionales.](#markdown-header-requerimientos-no-funcionales-que-debe-cumplir-toda-la-aplicacion-web)

5. [Decisiones técnicas](#markdown-header-decisiones-tecnicas)

    5.1 [Metodologías utilizadas y procesos definidos.](#markdown-header-metodologias-utilizadas-en-el-desarrollo-del-proyecto)

    5.2 [Artefactos utilizados en el desarrollo del proyecto.](#markdown-header-artefactos-utilizados-en-el-desarrollo-del-proyecto)

    5.3 [Tecnologías utilizadas con sus respectivas versiones.](#markdown-header-tecnologias-utilizadas-con-sus-respectivas-versiones)

    5.4 [Repositorio de código y estrategia git para el proyecto.](#markdown-header-repositorio-de-codigo-y-estrategia-git-para-el-proyecto)

    5.5 [Definición de listo.](#markdown-header-definicion-de-listo)

6. [Referencias bibliográficas](#markdown-header-referencias-bibliograficas)

## Definiciones, acrónimos y abreviaciones

* *CITIC:* Centro de Investigaciones en Tecnologías de Información y Comunicación.
* *UCR:* Universidad de Costa Rica.
* *ODI:* Oficina de Divulgación e Información
* *ECCI:* Escuela de Ciencias de Computación e Informática.
* *DDD:* Domain Driven Design.
* *VPN:* Virtual Private Network.



## Introducción

Este documento especifica la organización entre los equipos de trabajo del proyecto, cada uno de los roles de cada miembro, al igual que las decisiones técnicas para el desarrollo del proyecto. Se estructura en 3 secciones principales: **Listado de equipos, Descripción general del proyecto y Decisiones técnicas** .

## Listado de equipos y miembros de los equipos

### Equipo Panas
| Nombre  | Rol  |
| ------- | -----|
|Sebastián Montero Castro|ScrumMaster|
|Andrea Alvarado Acon|DataBase|
|Carlos Mora|Look and Feel|
|Dylan Arias|Git|
|Greivin Sánchez Garita|Ambassador and Documentation|

### Equipo Monkey Madness
| Nombre  | Rol  |
| ------- | -----|
|Nelsón Álvarez|Scrum Master and Database|
|Tyron Fonseca|Look and Feel|
|Rodrigo Contreras|Git|
|Roberto Méndez|Documentación|
|Dean Vargas|Ambassador|

### Equipo Cheetos
| Nombre  | Rol  |
| ------- | -----|
|Angie Sofia Castillo Campos|ScrumMaster|
|Sebastian Gonzalez Varela|Documentación|
|Oscar Navarro Céspedes|GitHub|
|Steven Núñez Murillo|Look and Feel|
|Esteban Quesada Quesada|Bases de Datos|
|Gabriel Revillat Zeledón|Ambassador|

### Equipo Pollos Hermanos
| Nombre  | Rol  |
| ------- | -----|
|Frank Alvarado Alfaro|Bases de Datos|
|Diana Luna Pacheco|Scrum Master|
|Pablo Otárola Rodríguez|Documentación|
|Christian Rojas Ríos|Look and Feel|
|David Sánchez López|Git|
|Elvis Badilla Mena|Ambassador|

## Descripción general

### Contexto Actual

El Centro de Investigaciones en Tecnologías de la Información y Comunicación de la UCR tiene desea promover la investigación en las áreas relacionadas con las TICs, 
es por esto que desea desarrollar una aplicación web para administrar la información de los grupos de investigación científica que trabajan en esta área y ayudar a 
facilitar la divulgación científica que realizan estos grupos.
Actualmente la Universidad de Costa Rica no cuenta con una única aplicación web para los distintos centros de investigación que existen. Es por esto que este proyecto plantea ser una primera solución escalable, que comience a utilizarse en el CITIC y luego pueda ser usada por los otros centros de investigación.

### Problema que resuelve

Dado la ausencia de una única aplicación web centralizada que permita acceder de manera sencilla y ordena a la información sobre proyectos de investigación, publicaciones, tesis, noticias 
de los centros de investigación de la UCR. Esta aplicación web viene a solucionar el problema de una información de difícil acceso y desactualizada que existe actualmente en el contexto de las diferentes páginas de los centros de investigación. 

### Interesados del proyecto y tipos de usuarios

En primera instancia la aplicación será desarrollada con un enfoque en el Centro de Investigaciones en Tecnologías de la Información y Comunicación (CITIC) de la UCR, de modo que las personas interasadas son todas aquellas que se relacionan directa o indirectamente con el CITIC
tales como: visitantes, personal administrativo e investigadores, estudiantes, publicadores, entre otros. 
Dado que la aplicación busca expandirse a los demás centros de investigación dentro de la universidad las personas interesadas son también todas aquellas que se verán beneficiadas de una aplicación centralizada, de fácil acceso, respuesta rápida e intuitiva.

### Solución propuesta

Desarrollar una aplicación web que permita administrar la información de los grupos de investigación científica y facilitar la divulgación científica que se realiza.

### Análisis del entorno.
- *Requerimientos del usuario:* 
    - El sistema permitirá acceder a la información del centro de investigación.
    - El sistema muestra la información de manera ordenada. 
    - El sistema permite el filtrado de información. 
    - La información presentada está actualizada al día. 

- *Regulación:* Este sistema se regula bajo las normas de la Universidad de Costa Rica. Será un sistema que cumpla con los lineamientos técnicos del centro de informática, así como los lineamientos formales establecidos por 
la Oficina de Divulgación e Información (ODI) y la Vicerectoría de Investigación. 

- *Competidores:*
    - Competidores Internos: 
        - Distintas páginas web existentes de los centros de investigación. 
        - Otros grupos de desarrollo dentro de la Universidad. 
    - Competidores Externos: 
        - Empresas Desarrolladoras de Software.
        - Desarrolladores Independientes. 
 

### Visión del producto

**Para** personas interesadas en proyectos de los Centros de investigación de la UCR **quienes requieren**  acceder a investigaciones de los diferentes centros y sus grupos de investigación de la UCR.
**El Sistema Grupos Investigación @UCR** es una **aplicación web** **que permite** la búsqueda de proyectos de investigación, publicaciones, tesis y personas investigadoras.
**De forma** distinta a las páginas actuales de centros de investigación de la UCR, **nuestro producto** proveerá un sitio web simple de usar, que facilitará la administración y publicación de la investigación que realizan los grupos de investigación de la UCR.

### Relación con otros sistemas externos

Los diversos centros de investigación de la UCR administran de manera distinta su información, principalmente en el acceso a los recursos que proporcionan, dado a que algunos casos tienen recursos de manera privativa como proyectos que se están realizando, tesis o publicaciones donde solo proporcionan un resumen. Además algunos de los sitios de los centros de investigación son lentos o tienen un look-and-feel distinto, con diseños que en ocasiones pueden llegar a dificultar el acceso a la información de interés. Nuestra aplicación viene a permitir centralizar los proyectos de investigación, publicaciones y tesis en un mismo sitio, para que pueda ser accedido por las personas interesadas, por medio de una interfaz amigable, y al igual que facilitar la administración proporcionando información estadística y a su vez permitiendo una mejor divulgación de la información científica.

### Descripción de los temas (módulos) asignados a cada equipo

* **Pollos Hermanos**: Publicaciones y estadísticas.
    - Publicaciones: Este módulo se encarga de gestionar las publicaciones del centro de investigación y de la visualización de cada una de estás publicaciones. 
    -Estadísticas: Este módulo se encarga de desplegar las estadísticas de la cantidad de publicaciones en cuatro apartados: por grupo de invstigación, por año, por área de investigación y por tipo de publicación.
* **MonkeyMadness**: Centro de investigación, Grupos de investigación, Áreas de investigación.
    - Centro de investigación: Este módulo contiene a la persona directora, personal administrativo y grupos de investigación asociados.
    - Grupos de investigación: Este módulo es el encargado de desplegar la información de personal administrativo y personas relacionadas (investigadores, estudiantes, colaboradores), además de las publicaciones, proyectos, tesis, entre otros.
    - Áreas de investigación: Este módulo contiene descripciones y palabras clave que permiten la clasificación de la información (proyectos, tesis, entre otros) que se encuentra dentro de los centros y grupos de investigación.
* **Flaming Hot Cheetos:** Proyectos de investigación y tesis de grado y posgrado.
    - Proyectos de investigación: Este módulo se encarga de la administración y visualización de los proyectos de investigación (con sus características y diferentes relaciones).
    - Tesis(grado y posgrado): Este módulo se encarga de la administración y visualización de las tesis de grado y posgrado (con sus características y diferentes relaciones).
* **Panas:** Personas, Noticias, Contacto, Trabajar con nosotros.
    - Personas: Este módulo se encarga de mostrar la información de contacto, publicaciones asociadas y rol dentro del centro de investigación para cada persona, así como de la administración de cuentas. Es el módulo más grande del equipo. 
    - Información Genereal: 
        - Noticias: Despliegue de la información relacionada a las noticias actualizadas del centro de investigación. 
        - Contacto: módulo encargado de brindar información pertinente al contacto con el centro de investigación. 
        - Trabaja con nosotros: brinda la información necesaria para conocer las labores que realiza el centro de investigación y cómo participar.  

### Requerimientos funcionales

Los requerimientos funcionales se administran por medio de la herramienta de **JIRA** el cual puede ser accedido por este [enlace](http://10.1.4.22:8080/projects/PIIB22021/summary). 

Para poder acceder a este proyecto de JIRA es necesario utilizar el **VPN proporcionado por la ECCI**, las instrucciones se pueden en este [enlace](https://www.ecci.ucr.ac.cr/colaboradores/procedimientos/acceso-remoto-por-red-privada-virtual-vpn-la-ecci).

### Mapa de ruta del producto
Si desea conocer el mapa de ruta del producto puede hacerlo accediendo a este [enlace]( https://docs.google.com/spreadsheets/d/1gA4VEfm1QP1bquN9UZCkp5slvnVyS1ng37YlMm5xcNM/edit?usp=sharing)

### Requerimientos no funcionales que debe cumplir toda la aplicación web.

* El sistema será desarrollado para navegadores web basados en Chromium y Gecko (Firefox).

* **Eficiencia:**

    * Toda la funcionalidad del sistema debe ser capaz de responder en menos de 10 segundos.

    * El sistema debe ser capaz de operar de manera adecuada con hasta 1000 usuarios de manera concurrente.
    
    * Los datos que se modifican deben ser actualizados en la base de datos para todos los usuarios en menos de 2 segundos. 

* **Usabilidad:**

    * El sistema debe proporcionar mensajes de error que sean informativos y orientados a usuario final.
    
    * El sistema debe poseer interfaces gráficas bien formadas.

    * El tiempo de aprendizaje del sistema por un usuario deberá ser menor a 4 horas.

* **Seguridad:**

    * El sistema se debe desarrollar aplicando recomendaciones de programación que mantengan la seguridad de los datos.
    
    * Los datos solo podrán ser modificados por el administrador del sistema o miembro con los permisos correspondientes a su rol.

* **Organización:**

    * La metodología de desarrollo de software será Domain Driven Development (DDD) y arquitectura limpia.

    * Cada semana se debera producir reportes de las reuniones diarias en los cuales cada equipo muestre los avances del proyecto.


## Decisiones técnicas

* Clean code: la especificación definida por los equipos desarrolladores se encuentra en:  [Código Limpio](https://docs.google.com/document/d/1HW9RavlhXJJ9bZ_XtidHbhkGfuQr4V2Q/edit?usp=sharing&ouid=105993574609311310782&rtpof=true&sd=true)

* Arquitectura Limpia: para la realización de este proyecto se trabajó en la arquitectura limpia de tipo DDD.  

    La decisión se toma en conjunto con el asesor técnico, que recomienda la arquitectura pues suele ir de la mano con Blazor. Es necesario recalcar que las capas exteriores pueden depender de las capas interiores, pero NO al revés. (Es decir, la capa de dominio no puede depender de la capa de presentación)

    Esta arquitectura cuenta con 4 capas bien definidas:

    * **Dominio**: Contiene las entidades, servicios del dominio, interfaces para repositorio.
    * **Aplicación**: Casos de uso y servicios de aplicación. (services)
    * **Infraestructura**: Implementaciones de las interfaces de dominio y fuentes de datos (Contextos).
    * **Presentación**: Interfaz de usuario, en general es quien interactúa con los servicios de la aplicación. (.razor)


### Metodologías utilizadas y procesos definidos

**Metodología ágil Scrum:**
Scrum es un marco de gestión para el desarrollo incremental de productos, valiéndose de uno o más equipos multifuncionales, autoorganizados, de aproximadamente siete personas cada uno. 
Estos equipos son responsables de la creación y adaptación de los procesos mediante una estructura de roles, reuniones, reglas, y artefactos para el trabajo en equipo que Scrum posee.
En Scrum se realizan entregas parciales y regulares del producto final, priorizadas por el beneficio que aportan al receptor del proyecto.
El mayor beneficio de la metodología Scrum se experimenta en el trabajo complejo que implica la creación de conocimiento y colaboración.

**Roles**: Se definieron distintos roles para trabajar en equipos transversales:  Ambassador, Encargado Git, Encargado Documentación, Encargado Look and Feel, Scrum Master.

**Reuniones**: Se decide realizar por lo menos una reunión semanal, con el objetivo de conocer los avances, problemas y sidebar topics que enfrenta cada equipo en el desarrollo de sus módulos. 

**Reglas**: Cada equipo es responsable de las reglas bajo las que se trabaja. Pero en conjunto todos deben cumplir con los requisitos de clean code, arquitectura limpia y definición de acordados.

**Artefactos**: En el siguiente punto del documento se encuentran los [artefactos utilizados](#markdown-header-artefactos-utilizados-en-el-desarrollo-del-proyecto). 

Cada equipo cuenta con un backlog de prioridades donde se almacenan las historias de usuario de acuerdo a su importancia.
Se trabaja mediante ciclos o iteraciones llamados sprints, estos consisten de períodos de trabajo continuo de entre 2 semanas y 2 meses. Al final de cada período se procede a una revisión del trabajo revisado con los inversores y el dueño del producto.

### Artefactos utilizados en el desarrollo del proyecto.

Se realizaron los siguientes artefactos para el diseño:

* [Modelo conceptual de la aplicación web](https://miro.com/app/board/o9J_ly1KQic=/)
* [Modelo conceptual y relacional base de datos](https://lucid.app/lucidchart/invitations/accept/inv_16199716-9634-40d5-9347-320976039096)

### Tecnologías utilizadas con sus respectivas versiones

* Microsoft Visual Studio Enterprise 2019 v16.11.2
* Microsoft SQL Management Studio 2018 v15.0.18386.0
* Microsoft SQL Server 
* Blazor 
* Asp.NET Core 5.0

### Repositorio de código y estrategia git para el proyecto

El código del proyecto se encuentra en un [repositorio de bitbucket](https://cristian_quesadalopez@bitbucket.org/cristian_quesadalopez/ecci_ci0128_ii2021_g01_pi.git)

Dado que se utilizó la metolodía ágil Scrum, se definió que se utilizaría una rama **main** de la cual se crea una rama por cada equipo de acuerdo a sus modúlos.
En cada subrama de equipo se crea una subrama por cada historia de usuario definida por los equipos. 

### Definición de listo

#### Para subir al master:
 
* No romper la arquitectura.
* Tiene que completar una tarea técnica.
* Comentario en el código especificando la última tarea técnica que modificó esa parte específica del código.
* Comentario explicando algo en el código que se considere complicado.
* Si se hace un cambio a los modelos se debe de hacer un pull request a los otros equipos.
* Seguir principios de clean code básico.
* Verificar que corra.
* Todas las tareas de integración deben tener pull requests entre los equipos involucrados.

#### Para presentar en el Sprint Review	estar en el master y actualizado en el backlog

* Tiene que estar cumpliendo los criterios de aceptación.
* Tiene que haber sido revisada y aprovada por el P.O. 

## Referencias bibliográficas

Elmasri, R. and Navathe, S., 2010. Fundamentals of database systems. 6th ed. Boston, United States: Addison-Wesley.
Cohn., M., 2005. Agile Estimating and Planning. 1st ed. Pearson India.

UML y Patrones, Craig Larman.
Ingeniería de Software, Roger Pressman.