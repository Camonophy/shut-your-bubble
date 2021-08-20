# shut-your-bubble


## Description

Remove text from speech bubbles in comics, cartoons, manga or similar graphical mediums to support fan translations.

   
## Dependencies

First, install Tesseract's english language package, so that the program is able to recognize english texts within images. 

### Ubuntu

```sh
sudo apt-get update && sudo apt-get install tesseract-ocr-eng
```
 
### Arch 
```sh
sudo pacman -Syu && sudo pacman -S tesseract-data-eng
```
 
### Windows
  
Use the Tesseract Windows installation from <a href="https://github.com/UB-Mannheim/tesseract/wiki">this repository</a>.

Add Tesseract-OCR to your PATH variable. Assuming you decide to use the installation for every user on your computer, you just have to add _C:\Program Files\Tesseract-OCR_. If you want to install Tesseract just for your own user profile, you have to add _C:\Users\USER_NAME\AppData\Local\Programs\Tesseract-OCR_ to your PATH variable by default.

#### Python
 After installing Tesseract on your machine, simply run:  
```sh
pip3 install -r requirements.txt
```
 
 to gather the required packages from the official Python Package Index. 

  
## Options: 



###### Following

    

