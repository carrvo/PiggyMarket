# Reporting

## Purpose
This component houses the passive assessment of
whether a user's lifestyle is affordable for them.
This component will be used for both day-to-day
updates as well as the period end, with an
emphasis on the latter.

## Structure
### Reporting.Interfaces
This project is for holding all of the publicly accessible type definitions.
This project also defines the meaning held by different types.
This project can be thought of as defining the component-level API.
## Reporting.Cmdlets
This project holds all `PowerShell` `cmdlets`, which are to be used
with the Factory Pattern to expose the underlying types that belong
to the type definitions in the `Reporting.Interfaces` project. This
will also be used to define higher-level functionality than the
state-ful `OOP` types. This project can be thought of as the
invokable component-level API.
Additionally, this project is the boundary for handing in
component-level configuration (and by "configuration" there
is the inclusion of handing in third-party dependencies whose
use is to be defined in the `Reporting.Interfaces` project).
## Reporting.Implementation
This project holds the concrete Implementation of the type definitions.
This project will use databases, and other third-party dependencies,
to build the business logic of the application.
## Reporting.UnitTests
This project holds all unit tests for all other projects.
