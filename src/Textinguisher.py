import cv2
import pytesseract 
import os

from PIL         import ImageDraw, Image
from pytesseract import Output

from PIL import Image

# Insert public functions here
__all__ = ["remove_section"]

'''
    Open an image and remove the text within the area of the
    given coordinations

    param image_path: Source path of the image
    param x, y:       Top left corner of the area
    param w, h:       Coordination of the bottom right corner
                      relevant to the top left corner
'''
def remove_section(image_path, x, y, w, h):

    # Default Tesseract system path for Windows machines
    if os.name == 'nt':
        pytesseract.pytesseract.tesseract_cmd = r'C:\Program Files\Tesseract-OCR\tesseract'

    binary_image  = load_binary_image(image_path)
    image_section = get_image_section(binary_image, x, y, w, h)
    data          = pytesseract.image_to_data(image_section, lang='eng', output_type=Output.DICT)

    # Remove entries containing no text
    word_indexes  = filter(lambda index: len(data['text'][index]) > 0, range(0, len(data['text'])))
    
    # Get the coordinations of the found texts
    word_box_cord = [(data['left'][i], data['top'][i], data['width'][i], data['height'][i]) for i in word_indexes]
    
    paint_over_text(word_box_cord, image_path, x, y)


'''
    Return a cropped section of an image

    param img:  Source path of the image
    param x, y: Top left corner of the area
    param w, h: Coordination of the bottom right corner
                relevant to the top left corner
'''
def get_image_section(img, x, y, w, h):
    return img[y: y + h, x: x + w]


'''
    Return an image, which is converted into a black and white only image, 
    where pixels are either 0 or 1

    param path: Source path of the image
'''
def load_binary_image(path):
    img_path = path
    image    = cv2.imread(img_path, 0)
    return cv2.threshold(image, 0, 255, cv2.THRESH_BINARY_INV + cv2.THRESH_OTSU)[1]


'''
    Paint over the recognized box around the text and save
    the image

    param word_box_coords:  Coordinates of the word box surrounding the text
    param image_path:       Source path of the image
    param x, y:             The top left corner of the cropped image
'''
def paint_over_text(word_box_coords, image_path, x, y):
    img = Image.open(image_path)
    draw = ImageDraw.Draw(img)

    for word_box in word_box_coords:
        draw.rectangle([(x + word_box[0], y + word_box[1]),      
                        (x + word_box[0] + word_box[2], y + word_box[1] + word_box[3])], 
                        outline = "white", 
                        fill    = (255,255,255) )
                    
    img.save(image_path)

# Example
if __name__ == "__main__":
    PATH = 'example/FEheroes.jpg'
    X, Y, W, H = 550, 123, 446, 49

    remove_section(PATH, X, Y, W, H)
