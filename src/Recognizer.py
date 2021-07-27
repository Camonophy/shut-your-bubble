# install pytesseract
# install tesseract
# install tesseract-data-eng / tesseract-ocr-eng

import cv2
import pytesseract 
from PIL import ImageDraw, Image
from pytesseract import Output
# import pytesseract as tess

from PIL import Image

def get_image_section(img, x, y, w, h):
    return img[y: y + h, x: x + w]

def load_binary_image(path):
    img_path = 'example/FEheroes.jpg'
    image    = cv2.imread(img_path, 0)
    return cv2.threshold(image, 0, 255, cv2.THRESH_BINARY_INV + cv2.THRESH_OTSU)[1]

def paint_over_text(word_box_coords, image_path):
    img = Image.open(image_path)
    draw = ImageDraw.Draw(img)

    for word_box in word_box_coords:
        draw.rectangle([(X + word_box[0], Y + word_box[1]),      
                        (X + word_box[0] + word_box[2], Y + word_box[1] + word_box[3])], 
                        outline = "white", 
                        fill    = (255,255,255) )
                    
    img.save(PATH)

PATH = 'example/FEheroes.jpg'
X, Y, W, H = 550, 123, 446, 49

image   = load_binary_image(PATH)
section = get_image_section(image, X, Y, W, H)
data    = pytesseract.image_to_data(section, lang='eng', output_type=Output.DICT)

word_indexes  = filter(lambda index: len(data['text'][index]) > 0, range(0, len(data['text'])))
word_box_cord = [(data['left'][i], data['top'][i], data['width'][i], data['height'][i]) for i in word_indexes]
    
paint_over_text(word_box_cord, PATH)