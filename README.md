# PiggyMarket
Financial Management App

## Introduction

## Legal and Desclaimer
### License
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

## Examples
These can be found under `Integration/UseCases/*.Tests.ps1`.

Note that their ordering, in terms of role pre-requisites (and
should help with understanding), are as follows:
1. Register
1. Administer
1. Configure
1. Purchaser
1. Tracker
1. Reviewer
1. Reporter

## Theory

# Contributing

## Design

### Input-Focused
The component-level API shall be defined in terms of its inputs.
This focus allows the output to be naturally defined by the component
it is meant to feed into.

Instead of providing expected outcomes that are to be consumed as a
dependency, each component (and the application's use as a third-party)
is expected to define its own independent needs and then use the
Adapter Pattern for another component (or the application) to be mapped to.
This is meant to create a divide between the component (or application)
and its dependencies, whereby the adapter acts as an implementation bridge.

The exclusion to this is any needs that are common to many components
may be consolidated into the API of the component providing those needs.

## UML

## Building
