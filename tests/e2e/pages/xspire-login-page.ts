/**
 * This utility module provides functions to interact with the Xspire login page.
 * It includes functions to navigate to the login page, perform login actions,
 * and verify the presence of error messages.
 */
import { fill, gotoURL, waitForPageLoadState } from 'utils/action-utils';
import { failureLoginCredentials, successLoginCredentials } from '../testdata/login-test-data';
import { expectElementToBeVisible, expectElementToHaveText } from 'utils/assert-utils';
import { getLocator, getLocatorByPlaceholder, getLocatorByText } from 'utils/locator-utils';

/**
 * Defines locators for the login page elements.
 * These locators are used to interact with the elements on the page.
 * The locators are defined using CSS selectors, XPath expressions,
 * or other locator strategies, and can be used to find elements
 * by their ID or placeholder attributes.
 */
export const imageBrandLogo = () => getLocator('xpath=//html/body/div/div/div/div/div/div/div[1]/div[1]');
export const stringBrandName = () => getLocator('xpath=//html/body/div/div/div/div/div/div/div[1]/div[2]');
export const stringFormHeader = () => getLocator('xpath=//html/body/div/div/div/div/div/div/div[2]/div/div[1]/h2');
export const comboLanguage = () => getLocator('xpath=//html/body/div/div/div/div/div/div/div[2]/div/div[1]/div/button');
export const stringTenant = () =>
  getLocator('xpath=//html/body/div/div/div/div/div/div/div[2]/div/div[2]/div/div[1]/span');
export const stringTenantName = () =>
  getLocator('xpath=//html/body/div/div/div/div/div/div/div[2]/div/div[2]/div/div[1]/h6/span');
export const buttonSwitchTenant = () =>
  getLocator(`#AbpTenantSwitchLink`).or(getLocatorByPlaceholder('AbpTenantSwitchLink', { exact: true }));
export const textboxUserName = () =>
  getLocator(`#LoginInput_UserNameOrEmailAddress`).or(
    getLocatorByPlaceholder('LoginInput_UserNameOrEmailAddress', { exact: true }),
  );
export const textboxPassword = () =>
  getLocator(`#password-input`).or(getLocatorByPlaceholder('Password', { exact: true }));
export const buttonPasswordVisiblity = () =>
  getLocator(`#PasswordVisibilityButton`).or(getLocatorByPlaceholder('PasswordVisibilityButton', { exact: true }));
export const checkboxRememberMe = () =>
  getLocator(`#LoginInput_RememberMe`).or(getLocatorByPlaceholder('LoginInput_RememberMe', { exact: true }));
export const stringRememberMe = () => getLocatorByText('Remember me', { exact: true });
export const linkForgotPassword = () => getLocator('xpath=//*[@id="loginForm"]/div[2]/div[2]/a');
export const buttonLogin = () => getLocator('xpath=//*[@id="loginForm"]/div[3]/button');
export const stringErrorAlert = () => getLocator('xpath=//*[@id="AbpPageAlerts"]/div');
export const buttonCloseErrorAlert = () => getLocator('xpath=//*[@id="AbpPageAlerts"]/div/button');
export const stringErrorUsername = () => getLocator(`#LoginInput_UserNameOrEmailAddress-error`);
export const stringErrorPassword = () => getLocator(`#password-input-error`);

/**
 * Defines common actions for interacting with the login page.
 * These actions include filling in the username and password fields,
 * clicking the login button, and verifying the presence of error messages.
 * The actions are defined as functions that can be called to perform
 * specific tasks on the login page.
 */
export async function navigateTo(strTargetedUrl: string) {
  await gotoURL(strTargetedUrl);
  await waitForPageLoadState();
}

export async function fillSuccessAccount() {
  await fill(textboxUserName(), successLoginCredentials.username);
  await fill(textboxPassword(), successLoginCredentials.password);
}

export async function fillFailedAccount() {
  await fill(textboxUserName(), failureLoginCredentials.username);
  await fill(textboxPassword(), failureLoginCredentials.password);
}

export async function fillEmptyUsername() {
  await fill(textboxPassword(), failureLoginCredentials.password);
}

export async function fillEmptyPassword() {
  await fill(textboxUserName(), failureLoginCredentials.username);
}

export async function verifyErrorMessage(mesageContent: string) {
  // error alert elelments should be visible
  await expectElementToBeVisible(stringErrorAlert());
  // content of message should be equal to the expected message
  await expectElementToHaveText(stringErrorAlert(), mesageContent);
}
