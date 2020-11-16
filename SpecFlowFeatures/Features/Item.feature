Feature: Item

Background: 
Given ItemTable has values
| Id                                   | Name     | Description      | Quantity |
| 12174E90-6797-4128-813F-F93428C3349C | Iphone 4 | This is iphone 4 | 10       |
| 1DD89817-9217-483D-96BC-5AB9DFF9AB7B | Iphone 5 | This is iphone 5 | 20       |
| 3643F5C8-39C7-43C1-8474-0FD9185A00E4 | Iphone 6 | This is iphone 6 | 30       |

@GetItem
Scenario: Get list of Item when enter Item page
Given HomePage is opened
When clicking Items on NavMenu
Then should see table like
| Id                                   | Name     | Description      | Quantity |
| 12174E90-6797-4128-813F-F93428C3349C | Iphone 4 | This is iphone 4 | 10       |
| 1DD89817-9217-483D-96BC-5AB9DFF9AB7B | Iphone 5 | This is iphone 5 | 20       |
| 3643F5C8-39C7-43C1-8474-0FD9185A00E4 | Iphone 6 | This is iphone 6 | 30       |
Then close browser

@CreateItem
Scenario: Create Item at Item page 
Given HomePage is opened
When clicking Items on NavMenu
And Enter Item like this
| Name     | Description      | Quantity |
| Iphone X | This is iphone X | 1        |
And Click Create button
Then shoul see table like
| Id                                   | Name     | Description      | Quantity |
| 12174E90-6797-4128-813F-F93428C3349C | Iphone 4 | This is iphone 4 | 10       |
| 1DD89817-9217-483D-96BC-5AB9DFF9AB7B | Iphone 5 | This is iphone 5 | 20       |
| 3643F5C8-39C7-43C1-8474-0FD9185A00E4 | Iphone 6 | This is iphone 6 | 30       |
| A4BB7863-2EE5-4C04-95F2-3CA1F607E47A | Iphone X | This is iphone X | 1        |
Then close browser