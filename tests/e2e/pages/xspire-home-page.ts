/**
 * This utility module provides functions to interact with the Xspire Home page.
 */
import { gotoURL, waitForPageLoadState } from 'utils/action-utils';
import { getLocator, getLocatorByText } from 'utils/locator-utils';

/**
 * Defines locators for the Home page elements.
 * These locators are used to interact with the elements on the page.
 * The locators are defined using CSS selectors, XPath expressions,
 * or other locator strategies, and can be used to find elements
 * by their ID or placeholder attributes.
 */
export const stringHeaderWelcome = () => getLocatorByText(/Welcome to Xspire/i).first();
export const imageUserAvatar = () => getLocator('xpath=//*[@id="userItem"]/a/div/img');
export const menuCtxItemLogout = () => getLocator('xpath=//*[@id="MenuItem_Account_Logout"]/a/span[2]');

/**
 * Defines common actions for interacting with the Home page.
 */
export async function navigateTo(strTargetedUrl: string) {
  await gotoURL(strTargetedUrl);
  await waitForPageLoadState();
}
