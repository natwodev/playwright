import { test } from 'globals/page-setup';
import {
  expectElementNotToBeChecked,
  expectElementToBeHidden,
  expectElementToBeVisible,
  expectElementToContainText,
  expectElementToHaveAttribute,
  expectElementToHaveText,
  expectPageToHaveTitle,
  expectPageToHaveURL,
} from 'utils/assert-utils';
import { waitForPageLoadState } from 'utils/action-utils';
import * as LoginPage from '../pages/xspire-login-page';
import * as HomePage from '../pages/xspire-home-page';

test.describe('TS-AUTH-001-Login Validation Suite', () => {
  test.beforeEach(async () => {
    // These steps will be executed before each test
  });

  test.afterEach(async () => {
    // These steps will be executed after each test
  });

  /**
   * Test Case ID: 	TC-LOGIN-001
   * Test Case Description: Validate login page visibility
   * Test Case Priority: 	Medium
   *
   * Pre-Conditions: None
   * Execution Steps:
   * 1. Navigate to the login page.
   * 2. Verify the visibility of the login page elements.
   * Expected Outcomes:
   * - The login page should be displayed with all elements visible.
   * Post-Conditions: None
   */
  test('TC-LOGIN-001 - Validate login page visibility', async () => {
    // Arrange
    await LoginPage.navigateTo('Account/Login');

    // Assert
    await expectPageToHaveTitle('Login | Xspire');
    await expectElementToBeVisible(LoginPage.imageBrandLogo());
    await expectElementToBeVisible(LoginPage.stringBrandName());
    await expectElementToHaveText(LoginPage.stringBrandName(), 'Xspire');
    await expectElementToBeVisible(LoginPage.stringFormHeader());
    await expectElementToHaveText(LoginPage.stringFormHeader(), 'Login');
    await expectElementToBeVisible(LoginPage.comboLanguage());
    await expectElementToContainText(LoginPage.comboLanguage(), 'English');
    await expectElementToBeVisible(LoginPage.stringTenant());
    await expectElementToHaveText(LoginPage.stringTenant(), 'Tenant');
    await expectElementToBeVisible(LoginPage.stringTenantName());
    await expectElementToHaveText(LoginPage.stringTenantName(), 'Not selected');
    await expectElementToBeVisible(LoginPage.buttonSwitchTenant());
    await expectElementToHaveText(LoginPage.buttonSwitchTenant(), 'switch');
    await expectElementToBeVisible(LoginPage.textboxUserName());
    //await expectElementToHaveText(LoginPage.textboxUserName(), 'User name or email address');
    await expectElementToBeVisible(LoginPage.textboxPassword());
    //await expectElementToHaveText(LoginPage.textboxPassword(), 'Password');
    await expectElementToBeVisible(LoginPage.buttonPasswordVisiblity());
    await expectElementToBeVisible(LoginPage.checkboxRememberMe());
    await expectElementNotToBeChecked(LoginPage.checkboxRememberMe());
    await expectElementToBeVisible(LoginPage.stringRememberMe());
    await expectElementToHaveText(LoginPage.stringRememberMe(), 'Remember me');
    await expectElementToBeVisible(LoginPage.linkForgotPassword());
    await expectElementToHaveText(LoginPage.linkForgotPassword(), 'Forgot password?');
    await expectElementToBeVisible(LoginPage.buttonLogin());
    await expectElementToContainText(LoginPage.buttonLogin(), 'Login');
    await expectElementToBeHidden(LoginPage.stringErrorAlert());
  });

  /**
   * Test Case ID: 	TC-LOGIN-002
   * Test Case Description: Should show validation errors for empty form submission
   * Test Case Priority: 	High
   *
   * Pre-Conditions: None
   * Execution Steps:
   * 1. Navigate to the login page.
   * 2. Click the login button without entering any credentials.
   * 3. Verify that the error message is displayed.
   * 4. Verify that the error message contains the text "The User name or email address field is required."
   * 5. Verify that the error message contains the text "The Password field is required."
   * 6. Verify that the error message is displayed for both username and password fields.
   * Expected Outcomes:
   * - The error message should be displayed with the text "The User name or email address field is required."
   * - The error message should be displayed with the text "The Password field is required."
   * - The error message should be displayed for both username and password fields.
   * Post-Conditions: None
   */
  test('TC-LOGIN-002 - Should show validation errors for empty form submission', async () => {
    // Arrange
    await LoginPage.navigateTo('Account/Login');

    // Act
    await LoginPage.buttonLogin().click();

    // Assert
    await expectElementToBeVisible(LoginPage.stringErrorUsername());
    await expectElementToHaveText(LoginPage.stringErrorUsername(), 'The User name or email address field is required.');
    await expectElementToBeVisible(LoginPage.stringErrorPassword());
    await expectElementToHaveText(LoginPage.stringErrorPassword(), 'The Password field is required.');
  });

  /**
   * Test Case ID: 	TC-LOGIN-003
   * Test Case Description: Should show validation error for empty username
   * Test Case Priority: 	High
   *
   * Pre-Conditions: None
   * Execution Steps:
   * 1. Navigate to the login page.
   * 2. Enter a valid password.
   * 3. Click the login button without entering a username.
   * 4. Verify that the error message is displayed.
   * 5. Verify that the error message contains the text "The User name or email address field is required."
   * Expected Outcomes:
   * - The error message should be displayed with the text "The User name or email address field is required."
   * - The error message should be displayed only for the username field.
   * Post-Conditions: None
   */
  test('TC-LOGIN-003 - Should show validation error for empty username', async () => {
    // Arrange
    await LoginPage.navigateTo('Account/Login');

    // Act
    await LoginPage.fillEmptyUsername();
    await LoginPage.buttonLogin().click();

    // Assert
    await expectElementToBeVisible(LoginPage.stringErrorUsername());
    await expectElementToHaveText(LoginPage.stringErrorUsername(), 'The User name or email address field is required.');
    await expectElementToBeHidden(LoginPage.stringErrorPassword());
  });

  /**
   * Test Case ID: 	TC-LOGIN-004
   * Test Case Description: Should show validation error for empty password
   * Test Case Priority: 	High
   *
   * Pre-Conditions: None
   * Execution Steps:
   * 1. Navigate to the login page.
   * 2. Enter a valid username.
   * 3. Click the login button without entering a password.
   * 4. Verify that the error message is displayed.
   * 5. Verify that the error message contains the text "The Password field is required."
   * Expected Outcomes:
   * - The error message should be displayed with the text "The Password field is required."
   * - The error message should be displayed only for the password field.
   * Post-Conditions: None
   */
  test('TC-LOGIN-004 - Should show validation error for empty password', async () => {
    // Arrange
    await LoginPage.navigateTo('Account/Login');

    // Act
    await LoginPage.fillEmptyPassword();
    await LoginPage.buttonLogin().click();

    // Assert
    await expectElementToBeVisible(LoginPage.stringErrorPassword());
    await expectElementToHaveText(LoginPage.stringErrorPassword(), 'The Password field is required.');
    await expectElementToBeHidden(LoginPage.stringErrorUsername());
  });

  /**
   * Test Case ID: 	TC-LOGIN-005
   * Test Case Description: Should show error for invalid credentials
   * Test Case Priority: 	High
   *
   * Pre-Conditions: None
   * Execution Steps:
   * 1. Navigate to the login page.
   * 2. Enter a invalid username and password.
   * 3. Click the login button.
   * 4. Verify that the error message is displayed.
   * 5. Verify that the error message contains the text "Invalid username or password!"
   * Expected Outcomes:
   * - The error message should be displayed with the text "Invalid username or password!"
   * - The error message should be displayed only for the password field.
   * Post-Conditions:
   * - Click on Close button of error alert
   * - Verify that the error message is hidden.
   */
  test('TC-LOGIN-005 - Should show error for invalid credentials', async () => {
    // Arrange
    await LoginPage.navigateTo('Account/Login');

    // Act
    await LoginPage.fillFailedAccount();
    await LoginPage.buttonLogin().click();

    // Assert
    await expectElementToBeVisible(LoginPage.stringErrorAlert());
    await expectElementToContainText(LoginPage.stringErrorAlert(), 'Invalid username or password!');

    // Post-Condition
    await LoginPage.buttonCloseErrorAlert().click();
    await expectElementToBeHidden(LoginPage.stringErrorAlert());
  });

  /**
   * Test Case ID: 	TC-LOGIN-006
   * Test Case Description: Should navigate to forgot password page
   * Test Case Priority: 	Medium
   *
   * Pre-Conditions: None
   * Execution Steps:
   * 1. Navigate to the login page.
   * 2. Click the "Forgot password?" link.
   * 3. Verify that the user is redirected to the forgot password page.
   * 4. Verify that the page title is "Forgot password? | Xspire Business Platform".
   * Expected Outcomes:
   * - The user should be redirected to the forgot password page.
   * - The page title should be "Forgot password? | Xspire Business Platform".
   * Post-Conditions: None
   */
  test('TC-LOGIN-006 - Should navigate to forgot password page', async () => {
    // Arrange
    await LoginPage.navigateTo('Account/Login');

    // Act
    await LoginPage.linkForgotPassword().click();

    // Assert
    await expectPageToHaveURL(/\/Account\/ForgotPassword/);
    await expectPageToHaveTitle('Forgot password? | Xspire');
  });

  /**
   * Test Case ID: 	TC-LOGIN-007
   * Test Case Description: Should handle password field as sensitive input
   * Test Case Priority: 	Medium
   *
   * Pre-Conditions: None
   * Execution Steps:
   * 1. Navigate to the login page.
   * 2. Verify that the password field is treated as sensitive input.
   * Expected Outcomes:
   * - The password field should be treated as sensitive input.
   * - The password field should not display the entered characters in plain text.
   * - The password field should have the attribute "type" set to "password".
   * Post-Conditions: None
   */
  test('TC-LOGIN-007 - Should handle password field as sensitive input', async () => {
    // Arrange
    await LoginPage.navigateTo('Account/Login');

    // Assert
    await expectElementToBeVisible(LoginPage.textboxPassword());
    await expectElementToHaveAttribute(LoginPage.textboxPassword(), 'type', 'password');
  });

  /**
   * Test Case ID: 	TC-LOGIN-008
   * Test Case Description: Should successfully login with valid credentials
   * Test Case Priority: 	High
   *
   * Pre-Conditions: None
   * Execution Steps:
   * 1. Navigate to the login page.
   * 2. Enter valid username and password.
   * 3. Click the login button.
   * 4. Verify that the user is logged in successfully.
   * 5. Verify that the user is redirected to the home page.
   * 6. Verify that the page title is "Xspire Business Platform".
   * Expected Outcomes:
   * - The user should be logged in successfully.
   * - The page title should be "Xspire Business Platform".
   * - The page contains the text "Welcome to Xspire Business Platform!".
   * Post-Conditions:
   * - Select Logout from the menu
   * - Verify that the user is logged out successfully.
   * - Verify that the user is redirected to the login page.
   * - Verify that the page title is "Login | Xspire Business Platform".
   * - Verify that the page URL is "Account/Login".
   */
  test('TC-LOGIN-008 - Should successfully login with valid credentials', async () => {
    // Arrange
    await LoginPage.navigateTo('Account/Login');

    // Act
    await LoginPage.fillSuccessAccount();
    await LoginPage.buttonLogin().click();

    // Assert
    await waitForPageLoadState();
    await expectPageToHaveTitle('Xspire');
    await expectElementToBeVisible(HomePage.imageUserAvatar());

    // Post-Condition
    await HomePage.imageUserAvatar().click();
    await HomePage.menuCtxItemLogout().click();
    await waitForPageLoadState();
    await expectPageToHaveTitle('Login | Xspire');
    await expectPageToHaveURL(/\/Account\/Login/);
  });
});
