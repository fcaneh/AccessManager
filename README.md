# Access Manager

Application console développée en C# permettant de simuler un système de contrôle d'accès physique.
Le projet applique les principes de Clean Architecture et met en œuvre plusieurs cas d'usage métier : gestion des utilisateurs, contrôle d'accès aux zones sécurisées, suivi des tentatives d'accès et statistiques.

## Fonctionnalités

- Vérification des accès à une zone sécurisée
- Gestion des utilisateurs
  - Création
  - Activation / désactivation
  - Recherche par badge
- Historique des demandes d'accès
- Consultation des demandes d'accès par utilisateur
- Statistiques globales
- Gestion de plusieurs niveaux d'autorisation

## Architecture

Le projet est organisé selon les principes de Clean Architecture.

- AccessManager.ConsoleApp
- AccessManager.Application
- AccessManager.Domain
- AccessManager.Infrastructure

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

## Cas d'usage implémentés

### Utilisateurs

- GetAllUsers
- GetUserByBadgeNumber
- CreateUser
- ToggleUserStatus

### Contrôle d'accès

- CheckAccess

### Historique

- GetAllAccessAttempts
- GetAccessAttemptsByBadgeNumber

### Statistiques

- GetStatistics

## Concepts utilisés

- Clean Architecture
- Repository Pattern
- CQRS léger
- Dependency Injection manuelle
- Records C#
- Séparation des responsabilités
- Gestion d'état métier

## Améliorations futures

- Persistance des données avec Entity Framework Core
- API REST ASP.NET Core
- Authentification
- Journalisation
- Tests unitaires
- Interface graphique

  
