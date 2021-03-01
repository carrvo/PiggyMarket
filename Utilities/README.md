# Utilities

## Purpose
This component houses extraneous `cmdlets` and all
third-party dependencies.

## Structure
## Utilities.Cmdlets
This project holds all `PowerShell` `cmdlets` that do not belong
in any other component.
## ThirdParty.Implementation
This project holds the concrete Implementation of the third-party
type definitions from other components. These will map the interfaces
defining third-party dependencies to an underlying third-party
library, which will then be consumed by the component.

This should be the *ONLY* project in the entire solution that directly
has any third-party libraries imported.
## ThirdParty.Cmdlets
This project holds all `PowerShell` `cmdlets` that select and expose
the third-party implementations. This project, then, constitutes the
boundary for how third-party dependencies are handed in to components.
