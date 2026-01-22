import axios from 'axios';

const apiUrl = import.meta.env.VITE_API_URL;

export const userLogin = async (email, password) => {
    try {
        const payload = {
            email: email,
            password: password
        };
        const response = await axios.post(`${apiUrl}/auth/login`, payload);
        localStorage.setItem('token', response.data.token)
        return response.data;
    } catch (error) {
        console.error("Error login in:", error);
        throw error;
    }
};

export const userRegister = async (email, password, firstName, middleName, lastName, secondLastName) => {
    try {
        const payload = {
            userDto: {
                email: email,
                password: password
            },
            userPersonalDataDto: {
                firstName: firstName,
                middleName: middleName,
                lastName: lastName,
                secondLastName: secondLastName
            }
        };
        const response = await axios.post(`${apiUrl}/auth/register`, payload);
        return response.data;
    } catch (error) {
        console.error("Error registering:", error);
        throw error;
    }
};