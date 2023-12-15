# Device Management API

This API allows users to manage devices by performing operations such as creating new devices and retrieving a list of existing devices.

## Base URL

The base URL for making requests to this API is: `http://localhost:7001`

### Prerequisites

Make sure you have Docker and Docker Compose installed on your machine.

### Steps to Run

1. Clone the repository:

   ```bash
   git clone https://github.com/HGattermeyer/DeviceManagement.git
2. Navigate to the project directory:

   ```bash
   cd DeviceManagement

3. Run Docker Compose:

   ```bash
   docker-compose up
   
4. Access the API:

Using Docker the API can be accessed at: http://localhost:7001/api/devices

## Postman Collection
A Postman collection file (DeviceManager.postman_collection.json) is provided in the root directory of this project for easier API testing and usage.

Steps to Use Postman Collection
Download the Postman collection file from the root directory of this project.
Import the collection file into Postman.
You'll now have pre-defined requests for interacting with the API.

## Endpoints

### GET Request

#### Retrieve Devices

- **Description:** Retrieve list of all devices.
- **Endpoint:** `/api/devices`
- **Method:** `GET`
- **Request:** No request body needed.
- **Response:**
  - **Success (200 OK):** Returns a JSON array of devices.
    ```json
    [
      {
        "id": 1,
        "name": "Device 1",
        "brand": "Brand A",
        "createdDate": "2023-12-15T10:26:41.219647Z"
      },
      {
        "id": 2,
        "name": "Device 2",
        "brand": "Brand B",
        "createdDate": "2023-12-15T10:26:37.952796Z"
      }
    ]
    ```
  - **Error Responses:** 
    - 404 Not Found: If there are no devices found.
   
### GET Device by ID

#### Retrieve Devices

- **Description:** Retrieve a specific device by its ID.
- **Endpoint:** `/api/devices/{id}`
- **Method:** `GET`
- **Request:** No request body needed.
- **Response:**
  - **Success (200 OK):** Returns the details of the device with the specified ID in JSON format.
    ```json
      {
        "id": 1,
        "name": "Device 1",
        "brand": "Brand A",
        "createdDate": "2023-12-15T10:26:41.219647Z"
      }
    ```
  - **Error Responses:** 
    - 404 Not Found: If there are no devices found.

### POST Request

#### Create Device

- **Description:** Create a new device.
- **Endpoint:** `/api/devices`
- **Method:** `POST`
- **Request:**
  - **Body:** JSON object with device information.
    ```json
    {
      "name": "New Device",
      "brandName": "Brand C"
    }
    ```
- **Response:**
  - **Success (201 Created):** Returns the created device details.
    ```json
    {
      "id": 3,
      "name": "New Device",
      "brand": "Brand C",
      "createdDate": "2023-12-15T10:26:41.2196476Z"
    }
    ```
  - **Error Responses:**
    - 400 Bad Request: If the request body is invalid.
    - 500 Internal Server Error: If there's a server-side issue.


### PUT Update Device

#### Update Device

- **Description:** Update an existing device by its ID.
- **Endpoint:** `/api/devices/{id}`
- **Method:** `PUT`
- **Request:**
  - **Body:** JSON object with device information.
    ```json
    {
      "name": "Updated Device",
      "brandName": "Brand D"
    }
    ```
- **Response:**
  - **Success (200 OK):** Returns the details of the updated device in JSON format.
    ```json
    {
      "id": 3,
      "name": "Updated Device",
      "brand": "Brand D",
      "createdDate": "2023-12-15T10:26:41.2196476Z"
    }
    ```
  - **Error Responses:**
    - 400 Bad Request: If the request body is invalid.
    - 404 Not Found: If the device with the specified ID does not exist.
    - 500 Internal Server Error: If there's a server-side issue.

### Delete Device by ID

#### Delete Device

- **Description:** Delete a specific device by its ID.
- **Endpoint:** `/api/devices/{id}`
- **Method:** `DELETE`
- **Request:** No request body needed.
- **Response:**
  - **Success (204 No Content):**
  - **Error Responses:** 
    - 404 Not Found: If there are no devices found.
   
### GET Devices by Brand

#### Get Devices

- **Description:** Retrieve a list of devices belonging to a specific brand.
- **Endpoint:** `/api/devices/brand/{brand}`
- **Method:** `GET`
- **Request:** No request body needed.
- **Response:**
  - **Success (200 OK):** Returns a JSON array containing details of devices belonging to the specified brand (e.g., "Apple").
  - **Error Responses:** 
    - 404 Not Found: If there are no devices found.
    

