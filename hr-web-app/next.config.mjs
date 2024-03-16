/** @type {import('next').NextConfig} */
const nextConfig = {
  experimental: {
      serverActions: true
    },
    images: {
      domains: ['localhost', 'cdn.pixabay.com', 'images.remotePatterns'],
    },
    output: 'standalone'
};

export default nextConfig;
