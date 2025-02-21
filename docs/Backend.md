#Documentation

Le backend est subdivisé en 4 parties principales : Domaine, Infrastructure, Application et Api. Le **Domaine** contient les entités de la base de données (Modèles). L'**infrastructure** contient la base de données et les répertoires. L'**Application** contient les services et la logique pour interagir avec la base de données. L'**Api** contient les contrôleurs et la logique pour interagir avec l'utilisateur.

> Lors de cet entretien, vous n'aurez pas à toucher à la partie Infrastructure et Domaine et devrez vous concentrer sur les parties Application et Api.

Veuillez noter que cette structure "Clean Architecture" en est une très simplifiée. Dans une application réelle, nous aurions plus d'abstractions et une plus grande séparation des préoccupations.

## L'Api

L'Api se compose uniquement de contrôleurs. Chaque contrôleur contient la logique pour interagir avec l'utilisateur et la logique pour exécuter les commandes. Chacun des points de terminaison des contrôleurs est documenté à l'aide de Swagger et peut également être trouvé ci-dessous.

### Books

`GET /api/book`<br>
Retourne l'ensemble des livres de l'application sous forme de `List<BookDto>`.

`POST /api/book`<br>
Crée un nouveau livre dans l'application et retourne ce dernier sous forme de `BookDto`.

`PUT /api/book/{id}`<br>
Met à jour le livre spécifié et retourne ce dernier sous forme de `BookDto`.

`DELETE /api/book/{id}`<br>
Supprime le livre spécifié.

### Members

`GET /api/member`<br>
Retourne l'ensemble des membres de l'application sous forme de `List<MemberDto>`.

`POST /api/member`<br>
Crée un nouveau membre dans l'application et retourne une valeur booléenne indiquant si l'opération a réussi.

`PUT /api/member/{id}`<br>
Met à jour le membre spécifié et retourne une valeur booléenne indiquant si l'opération a réussi.

### Loans

`GET /api/loan`<br>
Retourne l'ensemble des prêts de l'application sous forme de `List<LoanDto>`.


## L'application

L'application est subdivisée en 3 parties principales : Livres, Membres et Prêts. Chaque partie contient une commande et un dossier de modèles. Le dossier `Commands` contient les commandes pouvant être exécutées sur l'entité comme récupérer une liste, mettre à jour ou créer. Le dossier `Models` contient les DTOs _(Data Transfer Objects)_ retournés par les commandes.

Une commande se compose d'une classe `IRequest` qui contient les paramètres de la requête et d'une classe `IRequestHandler` qui contient la logique pour exécuter la commande.


## Le Domaine

Le domaine contient les modèles de la base de données. Chaque modèle est utilisé dans la portion `Infrastructure` de l'application afin de créer les tables de la base de données. Chaque modèle est également utilisé dans la portion `Application` de l'application afin de créer les DTOs retournés par les commandes.

## L'Infrastructure

L'infrastructure contient la base de données et les répertoires. La base de données est créée à l'aide de Entity Framework Core et contient les tables pour chaque modèle du domaine. Les répertoires contiennent les fichiers de configuration pour l'application. Le stockage des données est effectué à l'aide de SQLite sous la base de données `app.db` qui est créée automatiquement lors du démarrage de l'application. **Si toutefois vous souhaitez réinitialiser la base de données**, vous pouvez supprimer le fichier `app.db` et le recréer. Redémarrez ensuite l'application et la base de données sera recréée.
