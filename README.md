# Enterprise-computing-cw-2019-2020
# Aim
Build a share trader viewer application which can oversee trade, brokers and announcement notification. 

The main aim is towards the reuse of code, so techniques should be used to guarantee future extensibility of the project and also 
scout other projects/libraries for premade application parts. Thoughts need to be given on the possible incompatibilities between found components.

The final product will not be an active trading platform but rather a tracker, gathering information and notify users when conditions are met
A more in depth discussion of the coursework can be found in the [report](google.com)

# Build
The application has been built as a web app taking advantage of ASP .NET. MSOA was attempted in order to decoupling the application and create a
more flexible environment for future enhancements. Web API is utilized to create the backbone for the Services (features) where Api nodes controllers are set up to connect to services.
It it backed using SQL to manage the database. The project is set to be run on localhost environment.

# Design
The main design is as shown on the below picture. 
![design](https://github.com/Willyees/enterprise-computing-cw-2019-2020/blob/assets/assets/design.png)
Each service has its own database table containing only relevant information


Communication to the client and between the nodes is achieved by RESTFul Api connection.
![communication](https://github.com/Willyees/enterprise-computing-cw-2019-2020/blob/assets/assets/architecture.png)


# Features
- Secure registering and login
- View available shares, brokers and announcements

- Add/Remove interest on specific share and set wanted parameters to kick in the notification
![shares](https://github.com/Willyees/enterprise-computing-cw-2019-2020/blob/assets/assets/shares.jpg)

- Get real time notified in case share price is in the range of set parameters
![notification](https://github.com/Willyees/enterprise-computing-cw-2019-2020/blob/assets/assets/notification_shares.jpg)

- Real time database update of shares, broker and announcement information on the graphical web interface
- Recommend brokers based on the chosen interested shares (by using broker expertise and quality grade)
![reccomend_brokers](https://github.com/Willyees/enterprise-computing-cw-2019-2020/blob/assets/assets/brokers.jpg)
