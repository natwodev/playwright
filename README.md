## Introduction

Xspire full E2E Testing solution is based on Microsoft Playwright, Artillery which enables reliable end-to-end webapp functional and load testing.

## Features

- Easy to Configure
- Auto wait for all elements & checks
- Support webapp automation with support for chrome, Edge, Firefox and Safari
- Support load automation with web apps automation
- Support API testing (GET, POST, PUT, DELETE HTTP methods)
- Support Serial and Parallel execution
- Environment configuration using .env files

## Tech Stack/Libraries Used

- [PlayWright](https://playwright.dev/) - for web and api automation
- [Artillery](https://www.artillery.io/) - for load test automation
- [ESLint](https://eslint.org/) - pinpoint issues and guide you in rectifying potential problems in both JavaScript and TypeScript.
- [Prettier](https://prettier.io/) - for formatting code & maintain consistent style throughout codebase
- [Dotenv](https://www.dotenv.org/) - to load environment variables from .env file

## Project Structure

- `src`
  - `globals`
    - `custom-logger.ts` customized logging function for report generating
    - `global-setup.ts` global setUp method
    - `global-teardown.ts` global tearDown method
    - `optional-parameter_types.ts` defines parameter types which will be used in test code
    - `page-setup.ts` global page function
  - `utils`
    - `constants.ts` contains global constant files
    - `action-utils.ts` contains global utility for actions in test code
    - `assert-utils.ts` contains global utility for assertion in test code
    - `element-utils.ts` contains global utility for interaction with HTML elements in test code
    - `locator-utils.ts` contains global utility for finding HTML elements in test code
    - `page-utils.ts` contains global utility for interaction with HTML page in test code
    - `utils.ts` contains global utility function files
- `tests` contains test specitication functions
  - `api` contains api testing scripts
  - `e2e` contains end-to-end testing scripts
    - `pages` place to write separated pages which will be used in `specs` business scenario (specification) tests
    - `testdata` contains data for automation tests
    - `specs` contains business scenario tests
  - `load` contains load testing scripts

## Getting Started

### Prerequisite

- `nodejs`: Download and install [Node JS](https://nodejs.org/en/download) - version 22.14.0 or later
- `VS Code`: Download and install [VS Code](https://code.visualstudio.com/download)

### Installation

- Navigate to folder and install npm packages using:

  > `npm install`

- For first time installation use below command to download required browsers:

  > `npx playwright install`

- In case you want to do fresh setup of playwright
  - Create a folder & run command `npm init playwright@latest`
  - select `TypeScript` & select default for other options

### Usage

- For browser configuration, change required parameters in `playwright.config.ts` file

### How to generate Playwright code (Playwright Test Generator)

- run command `npx playwright codegen`
- Browser gets opened & navigate to web app & perform test actions

Playwright test generator generates tests and pick locator for you. It uses role,text and test ID locators.
To pick a locator, run the `codegen` command followed by URL, `npx playwright codegen https://www.google.com`

### Writing Tests

- Create test files in `tests/` folder

### Run Tests

#### To Run Webapp Tests via CLI

- `npm run test` to run test in ui mode - by default it runs in chromium headed mode (i.e., with the browser UI visible)

#### To Run Tests Multiple Times in Parallel

`npx playwright test --workers=5 --headed --repeat-each=5`

- This will run test 5 times, at a time 5 instance will run. `--workers=5` will run 5 instances

#### To Run Tests Multiple Times in Sequence

`npx playwright test --workers=1 --headed --repeat-each=5`

- This will run test 5 times, at a time single instance will run, `--repeat-each=5` will run 5 times

#### Grouping and Organizing Test Suite in PlayWright

`npx playwright test --grep @smoke` This will run only test tagged as @smoke

### Debug And Analyze

#### View Trace Result of PlayWright Execution

- Open `https://trace.playwright.dev`
- Upload `trace.zip` file to above site, it will show trace details

#### Run test in debug mode

`npx playwright test UIBasictest.spec.js --debug`

This will start running script in debug mode & open PlayWright inspector

#### Report Generation and Accessing Reports via CLI

Playwright Test offers several built-in reporters tailored for various requirements, along with the flexibility to integrate custom reporters. You can configure these reporters either through the command line or within the `playwright.config.ts` file. For a comprehensive guide on Playwright's in-built reporters, refer to the official [documentation](https://playwright.dev/docs/test-reporters).

- **Playwright command**: After executing tests, you can view the reports using the following command:

```bash
npx playwright show-report <path to the report>
```

- **Framework Configured script**: This framework's configuration for viewing reports is defined in the `package.json` under the `scripts` section:

```json
"report": "playwright show-report playwright-report"
```

To access the reports post-test execution using this configuration, run:

```bash
npm run report
```

### Best Practices in Test Authoring:

- `Create isolated test cases`: Each test case should be independent.
- `Write Meaningful Test Case Titles`: Make your test case titles descriptive and meaningful.
- `Follow the AAA (Arrange-Act-Assert) Pattern`: Align your Test-Driven Development (TDD) approach with the clarity of Arrange, Act, and Assert.
- `Maintain Cleanliness`: Separate additional logic from tests for a tidy and focused codebase.

## Documentation

- [Playwright Node.JS Documentation](https://playwright.dev/)
- [Artillery Documentation](https://www.artillery.io/docs)
- [Playwright API Reference](https://playwright.dev/docs/api/class-playwright)

## Troubleshooting
