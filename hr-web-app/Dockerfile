# قاعدة الصورة
FROM node:latest

# تحديث مجلد العمل
WORKDIR /usr/src/app

# نسخ ملف package.json و package-lock.json (إذا وجد) إلى مجلد العمل
COPY package*.json ./

# تثبيت التبعيات
RUN npm install

# نسخ بقية الملفات
COPY . .

# بناء التطبيق
RUN npm run build

# تشغيل التطبيق
CMD ["npm", "start"]
