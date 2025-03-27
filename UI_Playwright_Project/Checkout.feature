Feature: Checkout

Scenario: Verify Checkout process
	Given User opens Home Page
	When User selects a product item
	And User clicks on cart button
	Then User should see the product item in the cart details
	When User clicks on proceed payment button
	Then User should see the shipping form page opened
	When User fills the shipping form
