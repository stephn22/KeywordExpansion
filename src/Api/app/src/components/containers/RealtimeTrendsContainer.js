import * as React from 'react';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Container from '@mui/material/Container';
import Navbar from '../Navbar/Navbar';
import { Typography } from '@mui/material';

const mdTheme = createTheme();

export default function RealTimeTrendsContainer(props) {
    const title = props.title || 'RealTime Trends';

    return (
        <ThemeProvider theme={mdTheme}>
            <Box sx={{ display: 'flex' }}>

                <Navbar title={title} />

                <Box
                    component="main"
                    sx={{
                        backgroundColor: (theme) =>
                            theme.palette.mode === 'light'
                                ? theme.palette.grey[100]
                                : theme.palette.grey[900],
                        flexGrow: 1,
                        height: '100vh',
                        overflow: 'auto',
                    }}
                >
                    <Toolbar />
                    <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
                        <Typography variant='h5' component='div' gutterBottom>
                            RealTime Trends
                        </Typography>
                    </Container>
                </Box>
            </Box>
        </ThemeProvider>
    );
}