# Access Manager

Application console développée en C# permettant de simuler un système de contrôle d'accès physique.

Le projet applique les principes de Clean Architecture et met en œuvre plusieurs cas d'usage métier : gestion des utilisateurs, contrôle d'accès aux zones sécurisées, suivi des tentatives d'accès et statistiques.

## Objectif du projet

Ce projet a été réalisé dans le but de mettre en pratique :

* La Clean Architecture
* Le Repository Pattern
* La séparation des responsabilités
* L'organisation d'une application métier en couches
* La mise en œuvre de cas d'usage inspirés du pattern CQRS

## Fonctionnalités

* Vérification des accès à une zone sécurisée
* Gestion des utilisateurs

  * Création
  * Activation / désactivation
  * Recherche par badge
* Historique des demandes d'accès
* Consultation des demandes d'accès par utilisateur
* Statistiques globales
* Gestion de plusieurs niveaux d'autorisation

## Architecture

Le projet est organisé selon les principes de Clean Architecture :

* **AccessManager.ConsoleApp** : interface utilisateur console
* **AccessManager.Application** : cas d'usage et contrats
* **AccessManager.Domain** : entités métier et règles métier
* **AccessManager.Infrastructure** : implémentations techniques (repositories, données de démonstration)

### Structure du projet

```text
AccessManager
│
├── AccessManager.ConsoleApp
├── AccessManager.Application
│   ├── Features
│   └── Contracts
├── AccessManager.Domain
│   ├── Entities
│   └── Enums
└── AccessManager.Infrastructure
    ├── Repositories
    └── Seeders
```

## Cas d'usage implémentés

### Utilisateurs

* GetAllUsers
* GetUserByBadgeNumber
* CreateUser
* ToggleUserStatus

### Contrôle d'accès

* CheckAccess

### Historique

* GetAllAccessAttempts
* GetAccessAttemptsByBadgeNumber

### Statistiques

* GetStatistics

## Concepts utilisés

* Clean Architecture
* Repository Pattern
* Séparation des responsabilités
* Injection de dépendances manuelle
* Records C#
* LINQ
* Gestion d'état métier
* Organisation par cas d'usage

## Lancement du projet

```bash
dotnet build
dotnet run --project AccessManager.ConsoleApp
```

## Améliorations futures

* Persistance des données avec Entity Framework Core
* API REST ASP.NET Core
* Authentification
* Journalisation
* Tests unitaires
* Interface graphique

```
```
