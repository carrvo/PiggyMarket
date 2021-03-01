# Authenticating

## Purpose
This component houses all access control mechanisms.

## Structure
### Authenticating.Interfaces
This project is for holding all of the publicly accessible type definitions.
This project also defines the meaning held by different types.
This project can be thought of as defining the component-level API.
## Authenticating.Cmdlets
This project holds all `PowerShell` `cmdlets`, which are to be used
with the Factory Pattern to expose the underlying types that belong
to the type definitions in the `Authenticating.Interfaces` project. This
will also be used to define higher-level functionality than the
state-ful `OOP` types. This project can be thought of as the
invokable component-level API.
Additionally, this project is the boundary for handing in
component-level configuration (and by "configuration" there
is the inclusion of handing in third-party dependencies whose
use is to be defined in the `Authenticating.Interfaces` project).
## Authenticating.Implementation
This project holds the concrete Implementation of the type definitions.
This project will use databases, and other third-party dependencies,
to build the business logic of the application.
## Authenticating.UnitTests
This project holds all unit tests for all other projects.
