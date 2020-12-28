![logo](https://github.com/alanta/kontent-statiq-theme-planty/raw/main/input/images/logo.svg)

# Planty Kontent Statiq Theme

This theme can be used to create an e-commerce ready website. You'll get everything you need from products listing, categorization, FAQ, etc.

You can see it in action right [here](https://alanta.github.io/kontent-statiq-theme-planty/).

This theme uses [Kontent](https://kontent.ai) headless CMS, [Statiq](https://statiq.dev) site generator and [SnipCart](https://snipcart.com) e-commerce.

## Features

* Product management within Kontent
* Pages with sections
  * Sections: Hero, Header, Bullet points, Promotion, Store, Testimonials
  * Taxonomy : Tags and Categories
  * Ratings
* SEO support: open graph & twitter cards

## Getting Started

### Requirements

- [.NET Core 3.1](https://dotnet.microsoft.com/download)

### Clone the codebase

1. Click the ["Use this template"](https://github.com/alanta/kontent-statiq-theme-planty/generate) button to [create your own repository from this template](https://help.github.com/en/github/creating-cloning-and-archiving-repositories/creating-a-repository-from-a-template).

### Running locally

- `dotnet run -- preview`
  - You can also emulate running the project in a virtual directory by appending `--virtual-dir memoirs`. See all preview [options](https://statiq.dev/web/running/preview-server).
- Go to `http://localhost:5080/`

🎊🎉 **You are now ready to explore the code base!**

> By default, the content is loaded from a shared Kentico Kontent project. If you want to use your own clone of the project so that you can customize it and experiment with Kontent, continue to the next section.

### Create a content source

1. Go to [app.kontent.ai](https://app.kontent.ai) and [create an empty project](https://docs.kontent.ai/tutorials/set-up-kontent/projects/manage-projects#a-creating-projects)
1. Go to the "Project Settings", select API keys and copy the following keys for further reference
    - Project ID
    - Management API key
1. Use the [Template Manager UI](https://kentico.github.io/kontent-template-manager/import) for importing the content from [`content.zip`](./content.zip) file and API keys from previous step. Check *Publish language variants after import* option before import.

    > Alternatively, you can use the [Kontent Backup Manager](https://github.com/Kentico/kontent-backup-manager-js) and import data to the newly created project from [`content.zip`](./content.zip) file via command line:
    >
    >   ```sh
    >    npm i -g @kentico/kontent-backup-manager
    >
    >    kbm --action=restore --projectId=<Project ID> --apiKey=<Management API key> --zipFilename=content
    >    ```
    >
    > Go to your Kontent project and [publish all the imported items](https://docs.kontent.ai/tutorials/write-and-collaborate/publish-your-work/publish-content-items).

1. Map the codebase to the data source
    - adjust the `DeliveryOptions:ProjectId` key in `appSettings.json`

🚀 **You are now ready to use the site with your own Kentico Kontent project as data source!**

### Production deployment to GitHub pages

- Enable GitHub actions in your repo
- Copy the [`.github/workflows/dotnet-core.yml`](https://github.com/alanta/kontent-statiq-theme-planty/blob/master/.github/workflows/dotnet-core.yml) to your project
- Go to the repository secrets and set:
  - [`LinkRoot`](https://statiq.dev/framework/configuration/settings) to the relative path of your project (e.g. `/kontent-statiq-theme-planty`) - this is to ensure that all links work properly when deployed to a subfolder
  - [`Host`](https://statiq.dev/framework/configuration/settings) to the domain of your project (e.g. `domain.tld`) - this is to ensure that absolute links are generated where required

## Customizing

* Open `appsettings.json` to tweak the site name etc.
* _TODO_

## Credits 

This was forked from the Planty e-commerce theme for Stackbit created by [Snipcart](http://bit.ly/2YB7AUL), kudos to the Snip Cart team for building this.

Some images were provided by [Pexels](https://www.pexels.com/)