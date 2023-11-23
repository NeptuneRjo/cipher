<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a name="readme-top"></a>
<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Don't forget to give the project a star!
*** Thanks again! Now go create something AMAZING! :D
-->




<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/NeptuneRjo/cipher">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
  </a>

<h3 align="center">Cipher</h3>

  <p align="center">
    End-to-End encrypted chat application
    <br />
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
        <li><a href="#database">Database</a></li>
      </ul>
    </li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

[![Cipher Demo Gif][product-screenshot]](cipher-demo.gif)

Cipher seamlessly integrates the power of Razor Pages for a sleek user interface while prioritizing user privacy by encrypting messages from sender to recipient, 
ensuring confidential real-time communication. 
 
This chat application provides users with a secure way to connect and communicate in the digital realm, 
thanks to its intuitive design and robust security measures.


<p align="right">(<a href="#readme-top">back to top</a>)</p>



### Built With

* ASP.NET
* Razor Pages
* BootStrap
* SignalR
* XUnit

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple steps.

Cipher utilizes NTIER architecture for project organization.

The app is broken up into these projects:

1. `.API` - The Razor page, program and SignalR hubs
2. `.BLL` - The service layer and AutoMapper configuration
3. `.DAL` - Repositories, entityframework models, and input models for razor pages (and migrations)
4. `.DTO` - Data transfer objects
5. `.Test` - The XUnit tests for the service classes

### Prerequisites

1. An IDE configured for ASP.NET environments.

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/NeptuneRjo/cipher.git
   ```
2. In the .API project, configure the `appsettings.json` file
   ```json
   "ConnectionStrings": {
        "DefaultConnect": "my_connection_string"
   },
   "EncryptionSettings": {
        "Key": "aes_encryption_key" 
   },
   "Development": true to use SQL Memory Storage
   ```

### Database

1. Enter the .DAL project
    ```sh
    cd .\CipherApp.DAL\
    ```

2. Add a migration
    ```sh
    dotnet ef migrations add MigrationName --context DataContext --startup-project ..\CipherApp.API\
    ```

3. Apply the changes
    ```sh
    dotnet ef --startup-project ..\CipherApp.API\ database update --context DataContext
    ```

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/NeptuneRjo/cipher.svg?style=for-the-badge
[contributors-url]: https://github.com/NeptuneRjo/cipher/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/NeptuneRjo/cipher.svg?style=for-the-badge
[forks-url]: https://github.com/NeptuneRjo/cipher/network/members
[stars-shield]: https://img.shields.io/github/stars/NeptuneRjo/cipher.svg?style=for-the-badge
[stars-url]: https://github.com/NeptuneRjo/cipher/stargazers
[issues-shield]: https://img.shields.io/github/issues/NeptuneRjo/cipher.svg?style=for-the-badge
[issues-url]: https://github.com/NeptuneRjo/cipher/issues
[license-shield]: https://img.shields.io/github/license/NeptuneRjo/cipher.svg?style=for-the-badge
[license-url]: https://github.com/NeptuneRjo/cipher/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/linkedin_username
[product-screenshot]: images/screenshot.png
[Next.js]: https://img.shields.io/badge/next.js-000000?style=for-the-badge&logo=nextdotjs&logoColor=white
[Next-url]: https://nextjs.org/
[React.js]: https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB
[React-url]: https://reactjs.org/
[Vue.js]: https://img.shields.io/badge/Vue.js-35495E?style=for-the-badge&logo=vuedotjs&logoColor=4FC08D
[Vue-url]: https://vuejs.org/
[Angular.io]: https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white
[Angular-url]: https://angular.io/
[Svelte.dev]: https://img.shields.io/badge/Svelte-4A4A55?style=for-the-badge&logo=svelte&logoColor=FF3E00
[Svelte-url]: https://svelte.dev/
[Laravel.com]: https://img.shields.io/badge/Laravel-FF2D20?style=for-the-badge&logo=laravel&logoColor=white
[Laravel-url]: https://laravel.com
[Bootstrap.com]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white
[Bootstrap-url]: https://getbootstrap.com
[JQuery.com]: https://img.shields.io/badge/jQuery-0769AD?style=for-the-badge&logo=jquery&logoColor=white
[JQuery-url]: https://jquery.com 
