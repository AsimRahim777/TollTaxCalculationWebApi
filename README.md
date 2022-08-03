# TollTaxCalculationWebApi (.Net, C#)
This is a small .Net Webapi project to calculate Toll Tax.
Toll Tax Calculation (.Net, C#, WebApi)
Summary
Its a small level toll tax calculation services implementation project. I have created two apis. One is to be used for Vehicle data entry while they entered to the road, And the Get api will took all required fields from user and make calculation for Toll Tax and send the response back to the user with calculation data.
I didn’t connect application with database. I am just storing data into runtime Lists as this project is need to be run on local so that’s why I do not connect it with database. 
Bellow I mentioned the basic Rules and Entry Points Data.

Tolls are calculated based on the following:
1. Entry Point
2. Exit Point
3. Day of the week
4. Number plate in the format (LLL-NNN) where L is a letter and N is a number.
5. Special discount days

Rules: 
1. Toll tax has a base rate of rupees 20 (PKR) - this is a base rate that is added to the cost the moment a vehicle enters. 
2. For all distance traveled, the customer will be charge 0.2 rupees per KM 
3. The distance rate will be 1.5x on weekends (Sat/Sun) - determine at exit point 
4. For Mon and Wed, cars with even number in number plate will be given 10% discount, and for Tues and Thurs, cars with odd number in number plate will be given 10% discount - for Fri/Sat/Sun no difference on number plate - based on entry date/time 
5. On 3 national holidays discount will be given of 50% (23rd march, 14th August, and 25th December) 
6. The actual toll is collected when the vehicle exits the road.

Map of entry points, and distance from zero point:
Zero point: 0KM 
NS Interchange: 5KM 
Ph4 Interchange: 10KM 
Ferozpur Interchange: 17KM 
Lake City Interchange: 24KM 
Raiwand Interchange: 29KM 
Bahria Interchange: 34Km

Post Entry data

Api Payload and response
URL: http://localhost:55483/api/TollCalculation/PostEntry
Type: POST
Validation: All fields are required
Payload
{
    "InterChange": "Zero Point",
    "NumberPlate": "GAE-324",
    "Datetime": "08/03/2022"
}

Response
{
    "ResponseData": {
        "GuidId": "9e7fef44-e4af-47f6-8324-2f40e0237d43",
        "NumberPlate": "GAE-324",
        "InterChange": "Zero Point",
        "DateTime": "2022-08-03T00:00:00"
    },
    "HttpStatus": 200,
    "Message": "Record save successfully"
}
Postman Screenshot
 ![image](https://user-images.githubusercontent.com/74822047/182699423-0ac5e8da-d361-468e-a993-97f494bc2637.png)


Get Tax Calculation
URL: http://localhost:55483/api/TollCalculation/GetExitTax
Type: GET
Validation: All fields are required
Payload
{
     "InterChange": "NS Interchange",
    "NumberPlate": "GAE-124",
    "Datetime": "08/03/2022"
}

Response
{
 "ResponseData": {
        "BaseRate": 20.0,
        "DistanceCost": 1.0,
        "Discount": 2.1,
        "SubTotal": 21.0,
        "TotalCharges": 18.9
    },
    "HttpStatus": 200,
    "Message": "Tax Calculated"
}
Postman Screenshot
 ![image](https://user-images.githubusercontent.com/74822047/182699609-533eaa73-9af8-4c49-aced-f43b40904cd6.png)


