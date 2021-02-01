# Integration

## License
Copyright 2020 carrvo

This work may be distributed and/or modified under the
conditions of the LaTeX Project Public License, either version 1.3
of this license or (at your option) any later version.
The latest version of this license is in
  http://www.latex-project.org/lppl.txt
and version 1.3 or later is part of all distributions of LaTeX
version 2005/12/01 or later.

This work has the LPPL maintenance status \`maintained'.

The Current Maintainer of this work is carrvo.

The license may also be viewed through https://choosealicense.com/licenses/lppl-1.3c/

## Purpose
This component glues together the components, configuring
them and handing in dependencies, to form the application
as a whole. This component also provides the external,
application-level API.

## Structure
### Configuration
This holds all the `PowerShell` scripts used to configure
the application as a whole (including handing in dependencies)
and to glue the components together. The component-level API
`cmdlets` should be strung together in such a way that it
produces a fully running application. This is also where
third-party dependencies will be matched to their defined
component-level interface using the Adapter Pattern, so
that they can be handed in to the corresponding component.
### UseCases
This holds `PowerShell` `Pester` tests who constitute:
- Requirements Documentation
- Backlog (Features and User Stories)
- CLI API Definition
- Functional Tests
- Customer Examples
Most importantly, these tests constitute the authoritative source
for the features and functionality supported and not supported.

#### UseCases Structure
- The files will represent Actors' Personas or Roles
- The `Describe` will detail what role the Actor holds - WHO
- The `It` will describe a User Story or Scenario
- The body will have a sections labelled:
    1. `# Pre-Requisites` - all that is required for a runnable code example
    1. `# Requirement` - WHAT (and may hold verification)
    1. `# Rationale` - verification and WHY
- The tests themselves shall be considered the means to verify/test
- `PowerShell` defines a `Verb-Noun` (better understood as `Verb-Object`
with regards to Subject-Verb-Object composition of sentences in Linguistics)
that already defines WHAT for each requirement, but clarification
on the specific meaning and use of `Verb` choice is still recommended.
- Tests shall be visually ordered by increasing complexity, as per serving as
code examples

#### `Pester` Tags:
- Unique Identifier
- Priority/Iteration (`vXX.XX.XX`)
- Complexity: Simple, Moderate, Complex
- Goal or Operation, if not a Requirement
- Directives to Further Reading in the Architecture/Implementation

#### References
1. https://qracorp.com/write-clear-requirements-document/
1. https://developers.google.com/tech-writing/two/sample-code
1. https://reqexperts.com/wp-content/uploads/2015/07/writing_good_requirements.htm
1. https://softwareengineering.stackexchange.com/questions/163328/how-to-document-requirements-for-an-api-systematically

### IntegrationTests
This holds all `PowerShell` `Pester` tests that require `Moq`ing.
### FunctionalTests
This holds all `PowerShell` `Pester` tests that set up a fully
operational environment and run production-like user commands against.
Can also be thought of as "API Tests".
### REST
This holds the `PowerShell` `scripts` that defines and exposes the
application-level REST API, to be consumed by the `Client` component.
